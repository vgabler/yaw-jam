using System.Collections.Generic;
using UnityEngine;
using Yaw.Data;
using UnityEngine.UI;

namespace Yaw.Game
{
    /// <summary>
    /// Controla o deck e a combinação de runas.
    /// Valida a combinação feita e chama o summon.
    /// </summary>
    public class RuneBoardController : MonoBehaviour
    {
        public RuneUI runePickablePrefab;

        //As cartas que podem ser escolhidas
        public List<CardUI> activeCardsUI;
        public RectTransform runesContainer;
        public List<Button> runeSlots;
        RuneUI[] combination;

        public Summonner summoner;
        DeckData data;

        int currentCardIndex;

        RuneUI currentRune;

        List<CardData> activeCards;
        List<CardData> usedCards;
        List<CardData> deckCards;

        void Start()
        {
            var dataProvider = GetComponentInParent<ISingleDataProvider<DeckData>>();

            data = dataProvider.Data;

            //Atualiza as listas de carta
            deckCards = new List<CardData>(data.cards);
            activeCards = new List<CardData>();
            usedCards = new List<CardData>();

            UpdateActiveCards();
            UpdateRunesUI(data.runes);

            //Configura os slots da combinação
            foreach (var slot in runeSlots)
            {
                slot.onClick.AddListener(() => OnSlotClicked(slot));
            }
            combination = new RuneUI[Constants.RUNE_SLOTS];
        }

        void UpdateRunesUI(List<RuneDefinition> runes)
        {
            var toggleGroup = runesContainer.GetComponent<ToggleGroup>();

            foreach (var rune in runes)
            {
                //Cria a instância da runa
                var obj = Instantiate(runePickablePrefab, runesContainer);
                obj.SetUp(rune);
                //Adiciona listener do toggle
                var toggle = obj.GetComponent<Toggle>();
                toggle.group = toggleGroup;
                toggle.isOn = false;
                toggle.onValueChanged.AddListener((val) =>
                {
                    //Se está ativando, faz a chamada
                    if (val)
                    {
                        OnRunePicked(obj);
                    }
                    //Se for a runa ativa, e estiver desativando, deseleciona
                    else if (currentRune == obj)
                    {
                        currentRune = null;
                    }
                });
            }
        }

        /// <summary>
        /// Ao Selecionar uma runa, marca qual é a atual
        /// </summary>
        private void OnRunePicked(RuneUI obj)
        {
            currentRune = obj;
        }

        /// <summary>
        /// Ao clicar em um slot de runa, manda a que estiver selecionada para lá
        /// </summary>
        private void OnSlotClicked(Button slot)
        {
            var slotIndex = runeSlots.IndexOf(slot);
            //Se já tinha runa nesse slot, remove-a
            if (combination[slotIndex] != null)
            {
                var rune = combination[slotIndex];
                combination[slotIndex] = null;

                rune.transform.SetParent(runesContainer);

                //Reativa o raycast
                foreach (var item in rune.GetComponentsInChildren<Graphic>())
                {
                    item.raycastTarget = true;
                }
            }

            if (currentRune == null)
            {
                return;
            }

            //Se tem runa selecionada, passa essa runa para o slot
            combination[slotIndex] = currentRune;
            //Altera o parent
            var rectTransform = currentRune.GetComponent<RectTransform>();
            rectTransform.SetParent(slot.transform);
            //Faz ficar no meio do slot
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;

            //Desativa o raycast
            foreach (var item in currentRune.GetComponentsInChildren<Graphic>())
            {
                item.raycastTarget = false;
            }

            //Deseleciona
            currentRune.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            currentRune = null;
        }

        void UpdateActiveCards()
        {
            //Se não tem cards ativos, e o deck zerou, embaralha e começa de novo
            if (deckCards.Count <= 0 && activeCards.Count <= 0)
            {
                deckCards.AddRange(usedCards);
                deckCards.Shuffle();
                usedCards.Clear();
            }

            //Adiciona as 3 primeiras cartas às cartas ativas
            while (activeCards.Count < 3 && deckCards.Count > 0)
            {
                var card = deckCards[0];
                activeCards.Add(card);
                deckCards.RemoveAt(0);
            }

            //Atualiza o UI
            for (int i = 0; i < activeCardsUI.Count; i++)
            {
                //Se tiver carta ativa para esse indice, atualiza
                if (i < activeCards.Count)
                {
                    activeCardsUI[i].gameObject.SetActive(true);
                    activeCardsUI[i].SetUp(activeCards[i]);
                }
                //Se não, desabilita o card
                else
                {
                    activeCardsUI[i].gameObject.SetActive(false);
                }
            }
        }

        public void TrySummon()
        {
            //Aciona as cards ativas prontas pro summon
            for (int i = 0; i < activeCards.Count; i++)
            {
                var card = activeCards[i];

                if (VerifyCombination(card))
                {
                    //Se a combinação estiver certa, faz o summon
                    summoner.Summon(card.SummonData);
                    //Remove da pilha ativa
                    activeCards.RemoveAt(i);
                    //Adiciona na pilha de descarte
                    usedCards.Add(card);
                    i--;
                }
            }

            UpdateActiveCards();
        }

        public bool VerifyCombination(CardData card)
        {
            if (combination.Length != card.Combination.Length)
            {
                Debug.LogError("Combinações de tamanho diferente!");
                return false;
            }

            for (int i = 0; i < card.Combination.Length; i++)
            {
                if (card.Combination[i] != combination[i]?.Rune)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using Yaw.Data;
using UnityEngine.UI;

namespace Yaw.Game
{
    /// <summary>
    /// Controla a combinação de runas.
    /// </summary>
    public class RuneBoardController : MonoBehaviour
    {
        public RuneUI pickableRunePrefab;

        public RectTransform runesContainer;
        public List<Button> runeSlots;
        RuneUI[] combination;
        RuneUI currentRune;

        public DeckController deckController;
        public GameObject iDataProviderDeck;

        /// <summary>
        /// Busca as dependências, atualiza o UI e configura os slots
        /// </summary>
        void Start()
        {
            var dataProvider = iDataProviderDeck.GetComponent<ISingleDataProvider<DeckData>>();

            UpdateRunesUI(dataProvider.Data.runes);

            //Configura os slots da combinação
            foreach (var slot in runeSlots)
            {
                slot.onClick.AddListener(() => OnSlotClicked(slot));
            }
            combination = new RuneUI[Constants.RUNE_SLOTS];
        }

        /// <summary>
        /// Chamado ao clicar no botão "summon"
        /// </summary>
        public void SummonClicked()
        {
            //Cria o array da combinação e tenta sumonar alguma carta
            var c = new RuneDefinition[combination.Length];

            for (int i = 0; i < c.Length; i++)
            {
                c[i] = combination[i]?.Rune;
            }

            deckController.TrySummon(c);
        }

        /// <summary>
        /// Cria runas clicáveis de acordo com o necessário
        /// </summary>
        void UpdateRunesUI(List<RuneDefinition> runes)
        {
            var toggleGroup = runesContainer.GetComponent<ToggleGroup>();

            foreach (var rune in runes)
            {
                //Cria a instância da runa
                var obj = Instantiate(pickableRunePrefab, runesContainer);
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
    }
}
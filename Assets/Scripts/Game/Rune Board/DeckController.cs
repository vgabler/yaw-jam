using System;
using System.Collections.Generic;
using UnityEngine;
using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Controla o deck, cartas ativas e chama o summon
    /// </summary>
    public class DeckController : MonoBehaviour
    {
        public event Action OnCardsUpdated;

        public List<CardData> ActiveCards { get; private set; }
        public List<CardData> UsedCards { get; private set; }
        public List<CardData> DeckCards { get; private set; }

        //Referência do inspector
        public Summonner summoner;
        public GameObject iDataProviderDeck;
        DeckData data;

        /// <summary>
        /// Busca as dependências e inicializa o deck
        /// </summary>
        private void Start()
        {
            var dataProvider = iDataProviderDeck.GetComponent<ISingleDataProvider<DeckData>>();

            data = dataProvider.Data;

            //Atualiza as listas de carta
            DeckCards = new List<CardData>(data.cards);
            ActiveCards = new List<CardData>();
            UsedCards = new List<CardData>();

            UpdateActiveCards();
        }

        /// <summary>
        /// Atualiza as cartas ativas, adicionando até o máximo de 3
        /// </summary>
        void UpdateActiveCards()
        {
            //Se não tem cards ativos, e o deck zerou, embaralha e começa de novo
            if (DeckCards.Count <= 0 && ActiveCards.Count <= 0)
            {
                DeckCards.AddRange(UsedCards);
                DeckCards.Shuffle();
                UsedCards.Clear();
            }

            //Adiciona as 3 primeiras cartas às cartas ativas
            while (ActiveCards.Count < 3 && DeckCards.Count > 0)
            {
                var card = DeckCards[0];
                ActiveCards.Add(card);
                DeckCards.RemoveAt(0);
            }

            OnCardsUpdated?.Invoke();
        }

        /// <summary>
        /// Verifica se alguma das cartas ativas tem a combinação feita
        /// </summary>
        public void TrySummon(RuneDefinition[] combination)
        {
            //Aciona as cards ativas prontas pro summon
            for (int i = 0; i < ActiveCards.Count; i++)
            {
                var card = ActiveCards[i];

                if (card.VerifyCombination(combination))
                {
                    //Se a combinação estiver certa, faz o summon
                    summoner.Summon(card.SummonData);
                    //Remove da pilha ativa
                    ActiveCards.RemoveAt(i);
                    //Adiciona na pilha de descarte
                    UsedCards.Add(card);
                    i--;
                }
            }

            UpdateActiveCards();
        }
    }
}
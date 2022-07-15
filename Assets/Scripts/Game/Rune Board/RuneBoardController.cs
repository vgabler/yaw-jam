using System.Collections.Generic;
using UnityEngine;
using Yaw.Data;
using System.Linq;
namespace Yaw.Game
{
    /// <summary>
    /// Controla o deck e a combinação de runas.
    /// Valida a combinação feita e chama o summon.
    /// </summary>
    public class RuneBoardController : MonoBehaviour
    {
        public RuneUI runePerfab;
        public CardUI cardPrefab;

        //As cartas que podem ser escolhidas
        public RectTransform cardsContainer;
        public RectTransform runesContainer;

        public Summonner summoner;
        DeckData data;

        int currentCardIndex;

        void Start()
        {
            var dataProvider = GetComponentInParent<ISingleDataProvider<DeckData>>();

            data = dataProvider.Data;

            //Mostra as primeiras 3 cartas no container
            UpdateCardsUI(data.cards.Take(3).ToList());
            UpdateRunesUI(data.runes);
        }

        void UpdateCardsUI(List<CardData> cards)
        {
            foreach (var card in cards)
            {
                var obj = Instantiate(cardPrefab, cardsContainer);
                obj.SetUp(card);
            }
        }

        void UpdateRunesUI(List<RuneDefinition> runes)
        {
            foreach (var rune in runes)
            {
                var obj = Instantiate(runePerfab, runesContainer);
                obj.SetUp(rune);
            }

        }
    }
}
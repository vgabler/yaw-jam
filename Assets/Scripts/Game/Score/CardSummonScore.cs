using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Marca pontuação ao summonar uma carta
    /// </summary>
    public class CardSummonScore : MonoBehaviour
    {
        public DeckController deckController;
        IScoreController scoreController;
        void Start()
        {
            scoreController = ServiceLocator.Get<IScoreController>();
            deckController.OnCardSummoned += CardSummoned;
        }
        private void OnDestroy()
        {
            deckController.OnCardSummoned -= CardSummoned;
        }

        private void CardSummoned(CardData obj)
        {
            scoreController.AddScore(obj.ScoreValue);
        }
    }
}
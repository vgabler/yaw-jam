using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Yaw.Game
{
    /// <summary>
    /// Tela de resultados do fim de jogo
    /// </summary>
    public class GameOverScreen : MonoBehaviour
    {
        IGameStateController gameStateController;

        public Text resultText;
        void Start()
        {
            gameStateController = ServiceLocator.Get<IGameStateController>();

            if (gameStateController.State is GameStateEnded state)
            {
                var result = "";

                if (state.Reason is GameOverReasonVictory)
                {
                    result = "Victory!";
                }
                else if (state.Reason is GameOverReasonDefeat)
                {
                    result = "Defeat...";
                }

                resultText.text = result;
            }
        }

        public void Restart()
        {
            gameStateController.RestartGame();
        }
    }
}
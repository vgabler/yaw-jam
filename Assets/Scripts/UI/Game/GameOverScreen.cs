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
        IScoreController scoreController;

        public Text resultText;
        public Text scoreText;
        public Text bestScoreText;

        private void OnEnable()
        {
            gameStateController = ServiceLocator.Get<IGameStateController>();
            scoreController = ServiceLocator.Get<IScoreController>();

            UpdateUI();
        }

        void UpdateUI()
        {
            if (gameStateController.State is GameStateEnded state)
            {
                //Atualiza o texto de resultado
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
                //Atualiza os textos de pontuação
                scoreText.text = $"Score: {scoreController.Score}";
                bestScoreText.text = $"Best: {state.Data.BestScore}";
            }
        }

        public void Restart()
        {
            gameStateController.RestartGame();
        }
    }
}
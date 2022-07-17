using UnityEngine;
using UnityEngine.UI;

namespace Yaw.Game
{
    /// <summary>
    /// Contagem regressiva inicio do jogo
    /// </summary>
    public class GameStartingScreen : MonoBehaviour
    {
        IGameStateController gameStateController;

        public Text timerText;
        void Start()
        {
            gameStateController = ServiceLocator.Get<IGameStateController>();
        }

        void Update()
        {
            if (gameStateController.State is GameStateStarting state)
            {
                timerText.text = $"Starts in {Mathf.CeilToInt(state.RemainingTime)}";
            }
        }
    }
}
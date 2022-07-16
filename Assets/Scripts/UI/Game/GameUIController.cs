using UnityEngine;

namespace Yaw.Game
{
    /// <summary>
    /// Habilita / desabilita o conjunto de UI de acordo com o estado do jogo
    /// </summary>
    public class GameUIController : MonoBehaviour
    {
        IGameStateController gameStateController;
        public GameObject gameStartingUI;
        public GameObject gameRunningUI;
        public GameObject gameEndedUI;

        void Start()
        {
            gameStateController = ServiceLocator.Get<IGameStateController>();
        }

        //TODO reativo
        private void Update()
        {
            gameStartingUI.SetActive(gameStateController.State is GameStateStarting);
            gameRunningUI.SetActive(gameStateController.State is GameStateRunning);
            gameEndedUI.SetActive(gameStateController.State is GameStateEnded);
        }
    }
}
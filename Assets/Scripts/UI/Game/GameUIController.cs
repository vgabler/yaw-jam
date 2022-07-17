using UnityEngine;
using UnityEngine.UI;

namespace Yaw.Game
{
    /// <summary>
    /// Habilita / desabilita o conjunto de UI de acordo com o estado do jogo
    /// Também atualiza o texto de score
    /// </summary>
    public class GameUIController : MonoBehaviour
    {
        IScoreController scoreController;
        IGameStateController gameStateController;
        public GameObject gameStartingUI;
        public GameObject gameRunningUI;
        public GameObject gameEndedUI;
        public Text scoreText;

        void Start()
        {
            gameStateController = ServiceLocator.Get<IGameStateController>();
            scoreController = ServiceLocator.Get<IScoreController>();
        }

        //TODO reativo
        private void Update()
        {
            //Atualiza a tela visível
            gameStartingUI.SetActive(gameStateController.State is GameStateStarting);
            gameRunningUI.SetActive(gameStateController.State is GameStateRunning);
            gameEndedUI.SetActive(gameStateController.State is GameStateEnded);

            //Atualiza o texto de score
            scoreText.text = $"Score: {scoreController.Score}";
        }
    }
}
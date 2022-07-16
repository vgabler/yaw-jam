using UnityEngine;
using UnityEngine.UI;

namespace Yaw.Game
{
    /// <summary>
    /// Habilita / desabilita o botão de acordo com o estdo do jogo
    /// Chama o ForceQuit para finalizar a partida
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ForceQuitButton : MonoBehaviour
    {
        IGameStateController controller;

        Button btn;

        // Start is called before the first frame update
        void Start()
        {
            controller = ServiceLocator.Get<IGameStateController>();
            btn = GetComponent<Button>();
        }

        private void Update()
        {
            btn.interactable = controller.State is GameStateRunning;
        }

        public void EndGame()
        {
            controller.EndGame(new GameOverReasonForceQuit());
        }
    }
}
using UnityEngine;
using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Fica de olho na vida da torre; se chegar a zero, chama fim de jogo
    /// </summary>
    public class TowerController : MonoBehaviour
    {
        public int team;
        IGameStateController gameStateController;

        ISingleDataProvider<TowerData> provider;

        void Start()
        {
            gameStateController = ServiceLocator.Get<IGameStateController>();
            provider = GetComponentInParent<ISingleDataProvider<TowerData>>();
        }

        void Update()
        {
            //Se o jogo não estiver no state rodando, ignora
            if (!(gameStateController.State is GameStateRunning))
            {
                return;
            }

            //Se essa torre morreu, chama o gameover
            if (provider.Data.Health <= 0)
            {
                IGameOverReason reason;

                //TODO não é a melhor forma de verificar isso..
                if (team == 0)
                {
                    reason = new GameOverReasonDefeat();
                }
                else
                {
                    reason = new GameOverReasonVictory();
                }

                gameStateController.EndGame(reason);
            }
        }
    }
}
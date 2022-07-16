using System.Collections;
using UnityEngine;
using Yaw.Data;
using Yaw.Navigation;

namespace Yaw.Game
{
    public class GameStateControllerImpl : MonoBehaviour, IGameStateController
    {
        //Referência do Inspector
        public float startDelay = 2;

        //Dependências
        INavigationController navigationController;

        //Campos
        StageData currentStageData;
        public IGameState State { get; private set; }

        /// <summary>
        /// Inicializa o estado e registra o controller
        /// </summary>
        private void Awake()
        {
            State = new GameStateIdle();
            ServiceLocator.Register<IGameStateController>(this);
        }

        /// <summary>
        /// Busca as dependências
        /// </summary>
        private void Start()
        {
            navigationController = ServiceLocator.Get<INavigationController>();
        }

        /// <summary>
        /// Atualiza o estado e carrega a cena do jogo
        /// </summary>
        public void StartGame(StageData stage)
        {
            currentStageData = stage;
            State = new GameStateStarting(currentStageData);
            navigationController.ChangeScene("Game");
            StartCoroutine(StartDelayRoutine(startDelay));
        }

        /// <summary>
        /// Reinicia a mesma fase
        /// </summary>
        public void RestartGame()
        {
            StartGame(currentStageData);
        }

        /// <summary>
        /// Finaliza a partida
        /// </summary>
        public void EndGame(IGameOverReason reason)
        {
            if (reason is GameOverReasonVictory victory)
            {
                currentStageData.Completed = true;
            }

            State = new GameStateEnded(currentStageData, reason);

            //Se for para forçar a saida, não salva os resultados
            if (reason is GameOverReasonForceQuit)
            {
                navigationController.ChangeScene("Home");
            }

            //TODO salvar os resultados da partida
        }

        IEnumerator StartDelayRoutine(float duration)
        {
            var timer = duration;

            while (timer > 0)
            {
                (State as GameStateStarting).RemainingTime = timer;
                timer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            State = new GameStateRunning(currentStageData);
        }
    }
}
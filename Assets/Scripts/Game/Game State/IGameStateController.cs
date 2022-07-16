using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Gerencia o estado do jogo
    /// </summary>
    public interface IGameStateController
    {
        //TODO melhor seria reativo

        /// <summary>
        /// Estado atual do jogo
        /// </summary>
        IGameState State { get; }

        /// <summary>
        /// Inicia uma nova partida de acordo com os dados da fase
        /// </summary>
        void StartGame(StageData stage);

        /// <summary>
        /// Inicia uma nova partida com os mesmos dados da última
        /// </summary>
        void RestartGame();

        /// <summary>
        /// Finaliza a partida por algum motivo
        /// </summary>
        void EndGame(IGameOverReason reason);
    }

    /// <summary>
    /// Representa o estado atual do jogo
    /// </summary>
    public interface IGameState { }

    /// <summary>
    /// Representa o motivo para o jogo terminar
    /// </summary>
    public interface IGameOverReason { }
}
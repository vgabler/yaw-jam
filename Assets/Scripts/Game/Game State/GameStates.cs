
using Yaw.Data;

namespace Yaw.Game
{
    //TODO classes imutáveis

    /// <summary>
    /// Gerenciar o estado dessa forma permite aproveitar o polimorfismo para criar estados mais complexos
    /// </summary>
    public abstract class GameState : IGameState
    {
        public StageData Data { get; private set; }

        public GameState(StageData data)
        {
            this.Data = data;
        }
    }

    /// <summary>
    /// Partida não inicializou, ou já terminou
    /// </summary>
    public class GameStateIdle : GameState
    {
        public GameStateIdle() : base(null) { }
    }

    /// <summary>
    /// Partida no processo de inicialização
    /// </summary>
    public class GameStateStarting : GameState
    {
        public float RemainingTime { get; internal set; }
        public GameStateStarting(StageData data) : base(data) { }
    }

    /// <summary>
    /// Partida em andamento
    /// </summary>
    public class GameStateRunning : GameState
    {
        public GameStateRunning(StageData data) : base(data) { }
    }

    /// <summary>
    /// Partida Finalizada
    /// </summary>
    public class GameStateEnded : GameState
    {
        public IGameOverReason Reason { get; private set; }
        public GameStateEnded(StageData data, IGameOverReason reason) : base(data)
        {
            this.Reason = reason;
        }
    }
}
namespace Yaw.Game
{
    public enum SummonState { Idle, Walking, Attacking, Dying, Dead }

    /// <summary>
    /// Gerencia o estado do Summon
    /// </summary>
    public interface ISummonStateController
    {
        /// <summary>
        /// Estado atual do Summon
        /// </summary>
        public SummonState State { get; }

        /// <summary>
        /// Tenta alterar o estado, retorna verdadeiro se alterou.
        /// </summary>
        public bool TryChangeState(SummonState state);
    }
}
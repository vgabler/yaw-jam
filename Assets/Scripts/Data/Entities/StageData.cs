using System;
namespace Yaw.Data
{
    /// <summary>
    /// Dados de uma fase específica. Se terminou, melhor pontuação, etc.
    /// </summary>
    [Serializable]
    public struct StageData
    {
        [NonSerialized] //Esse valor vai ser inserido ao carregar as fases
        public StageDefinition Definition;
        public bool Completed;
        public bool Locked;
        public int BestScore;
    }
}
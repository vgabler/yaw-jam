namespace Yaw.Data
{
    /// <summary>
    /// Dados de uma fase específica. Se terminou, melhor pontuação, etc.
    /// </summary>
    [System.Serializable]
    public class StageData
    {
        public StageDefinition Definition;
        public bool Completed;
        public bool Locked;
    }
}
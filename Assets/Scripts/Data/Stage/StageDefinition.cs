using UnityEngine;

namespace Yaw.Data
{
    /// <summary>
    /// Definição de fase. Nome, inimigos, dificuldade, etc
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Stage Definition")]
    public class StageDefinition : ScriptableObject
    {
        public string stageName;
    }
}
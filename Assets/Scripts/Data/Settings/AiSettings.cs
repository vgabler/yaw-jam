using UnityEngine;

namespace Yaw.Data
{
    /// <summary>
    /// Configurações de AI, fica dentro da fase
    /// </summary>
    [CreateAssetMenu(menuName = "Game/AI Settings")]
    public class AiSettings : ScriptableObject
    {
        [Tooltip("Espera antes de começar a jogar")]
        public float StartDelay = 5;

        [Tooltip("Tempo para colocar cada runa na combinação")]
        public float RunePlacementDelay = 1;

        [Tooltip("Tempo para esperar quando realizar o summon")]
        public float SummonDelay = 3;
    }
}
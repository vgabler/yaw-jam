using System.Collections.Generic;
using UnityEngine;

namespace Yaw.Data
{
    /// <summary>
    /// Definição de runa
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Card")]
    public class CardDefinition : ScriptableObject
    {
        public SummonData SummonData;

        [Tooltip("Essas runas vão ser distribuidas aleatoriamente nos slots disponíveis")]
        public List<RuneDefinition> Runes;
    }
}
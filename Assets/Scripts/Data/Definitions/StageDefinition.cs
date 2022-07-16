using System.Collections.Generic;
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

        [Tooltip("O indice da lista representa o time. 0 - player, 1 - cpu")]
        public List<DeckDefinition> TeamDecks;
    }
}
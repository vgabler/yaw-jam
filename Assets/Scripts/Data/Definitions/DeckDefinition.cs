using System.Collections.Generic;
using UnityEngine;

namespace Yaw.Data
{
    /// <summary>
    /// Definição de Deck, ou conjunto de cartas
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Deck")]
    public class DeckDefinition : ScriptableObject
    {
        public List<CardDefinition> cards;
    }
}
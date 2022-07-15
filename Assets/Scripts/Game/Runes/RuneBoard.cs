using UnityEngine;
using Yaw.Data;

namespace Yaw.Game
{
    public class RuneBoard : MonoBehaviour
    {
        public DeckDefinition deck;
        DeckData data;

        void Start()
        {
            data = new DeckData(deck);
            Debug.Log("Carregou as cartinea!");
        }
    }
}
using UnityEngine;

namespace Yaw.Data
{
    /// <summary>
    /// Gerencia os dados do deck
    /// </summary>
    public class DeckDataProvider : MonoBehaviour, ISingleDataProvider<DeckData>
    {
        //TODO apenas para teste
        public DeckDefinition testDef;

        public DeckData Data { get; set; }

        private void Awake()
        {
            Set(new DeckData(testDef));

        }

        public void Set(DeckData value)
        {
            Data = value;
        }
    }
}
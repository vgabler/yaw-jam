using UnityEngine;
using UnityEngine.UI;
using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Atualiza as informações UI do summon
    /// </summary>
    public class TowerUI : MonoBehaviour
    {
        public Text healthText;
        ISingleDataProvider<TowerData> provider;

        //TODO dependency injection
        void Start()
        {
            provider = GetComponentInParent<ISingleDataProvider<TowerData>>();
        }

        //TODO reactive
        void Update()
        {
            var data = provider.Data;
            healthText.text = data.Health.ToString();
        }
    }
}
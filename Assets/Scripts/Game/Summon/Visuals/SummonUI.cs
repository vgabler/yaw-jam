using UnityEngine;
using UnityEngine.UI;
using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Atualiza as informações UI do summon
    /// Usado tanto para as cartas quanto para os summons ingame
    /// </summary>
    public class SummonUI : MonoBehaviour
    {
        public Text healthText;
        public Text attackText;
        ISingleDataProvider<SummonData> provider;

        //TODO dependency injection
        void Start()
        {
            provider = GetComponentInParent<ISingleDataProvider<SummonData>>();
        }

        //TODO reactive
        void Update()
        {
            var data = provider.Data;
            healthText.text = data.Health.ToString();
            attackText.text = data.Attack.ToString();
        }
    }
}
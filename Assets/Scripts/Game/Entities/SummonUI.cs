using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaw.Data;

namespace Yaw.Game
{
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

        //TODO atualizar só quando houver alteração
        void Update()
        {
            var data = provider.Get();
            healthText.text = data.Health.ToString();
            attackText.text = data.Attack.ToString();
        }
    }
}
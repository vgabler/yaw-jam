using UnityEngine;
using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Atualiza o visual do summon.
    /// Vira o objeto, atualiza sprite, particulas, etc
    /// </summary>
    public class SummonVisuals : MonoBehaviour
    {
        public Transform body;
        ISingleDataProvider<SummonData> provider;

        //TODO dependency injection
        void Start()
        {
            provider = GetComponentInParent<ISingleDataProvider<SummonData>>();
            var data = provider.Data;

            //TODO reatividade; pode ser que altere o time?
            //Inverte a direção do personagem
            if (!data.IsFromLeft)
            {
                var scale = body.localScale;
                scale.x *= -1;
                body.localScale = scale;
            }
        }
    }
}
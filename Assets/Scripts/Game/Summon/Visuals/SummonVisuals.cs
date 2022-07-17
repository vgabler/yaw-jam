using UnityEngine;
using Yaw.Data;
using Yaw.Utils;

namespace Yaw.Game
{
    /// <summary>
    /// Atualiza o visual do summon.
    /// Vira o objeto, atualiza sprite, particulas, etc
    /// </summary>
    public class SummonVisuals : MonoBehaviour
    {
        public Color[] teamColors;
        public Transform body;
        ISingleDataProvider<SummonData> provider;

        //TODO dependency injection
        void Start()
        {
            provider = GetComponentInParent<ISingleDataProvider<SummonData>>();
            var data = provider.Data;

            //Inverte a direção do personagem
            if (!data.IsFromLeft)
            {
                //TODO reatividade; pode ser que altere o time?
                var scale = body.transform.localScale;
                scale.x *= -1;
                body.transform.localScale = scale;
            }

            //Atribui a cor do time
            GetComponent<SpriteRendererGroup>().SetColour(teamColors[data.Team]);
        }
    }
}
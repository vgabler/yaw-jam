using System.Collections.Generic;
using UnityEngine;

namespace Yaw.Utils
{
    /// <summary>
    /// Gerencia vários SpriteRenderers ao mesmo tempo
    /// Faz alterações no edit mode
    /// </summary>
    [ExecuteInEditMode]
    public class SpriteRendererGroup : MonoBehaviour
    {
        public List<SpriteRenderer> renderers = new List<SpriteRenderer>();

        //Para alterar por script, usa essa propriedade
        public Color Color
        {
            get => color; set
            {
                SetColour(value);
            }
        }

        [SerializeField] Color color;

#if UNITY_EDITOR
        Color lastColor;

        /// <summary>
        /// Somente no editor, fica verificando se houve mudança na cor para atualizar
        /// </summary>
        public void Update()
        {
            //Mexeu no inspector
            if (lastColor != color)
            {
                SetColour(color);
            }
        }
#endif
        /// <summary>
        /// Atualiza a cor em cada renderer
        /// </summary>
        public void SetColour(Color color)
        {
            this.color = color;
            foreach (var sr in renderers)
            {
                sr.color = color;
            }
            lastColor = color;
        }
    }
}
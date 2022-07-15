using UnityEngine;
using UnityEngine.UI;
using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Exibição de uma runa
    /// </summary>
    public class RuneUI : MonoBehaviour
    {
        public Image image;

        internal void SetUp(RuneDefinition runeDefinition)
        {
            if (image != null)
            {
                image.sprite = runeDefinition.Sprite;
            }
        }
    }
}
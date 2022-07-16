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
        public RuneDefinition Rune { get; private set; }
        public Image image;

        public void SetUp(RuneDefinition runeDefinition)
        {
            this.Rune = runeDefinition;
            if (image != null)
            {
                image.sprite = runeDefinition.Sprite;
            }
        }
    }
}
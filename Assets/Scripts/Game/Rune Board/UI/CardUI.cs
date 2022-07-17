using UnityEngine;
using UnityEngine.UI;
using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Exibição das cartas
    /// </summary>
    public class CardUI : MonoBehaviour
    {
        public Text healthText;
        public Text attackText;
        public Text nameText;
        public Image avatar;
        public RuneCombinationUI combinationUI;

        /// <summary>
        /// Atualiza os elementos UI
        /// </summary>
        public void SetUp(CardData data)
        {
            if (healthText != null)
            {
                healthText.text = $"{data.SummonData.Health}";
            }

            if (attackText != null)
            {
                attackText.text = $"{data.SummonData.Attack}";
            }

            if (nameText != null)
            {
                nameText.text = data.Name;
            }

            if (combinationUI != null)
            {
                combinationUI.SetUp(data.Combination);
            }

            if (avatar != null)
            {
                avatar.sprite = data.SummonData.Avatar;
            }
        }
    }
}
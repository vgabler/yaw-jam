using System;
using UnityEngine;
using UnityEngine.UI;
using Yaw.Data;

namespace Yaw.StageSelect
{
    /// <summary>
    /// Entrada do seletor de fase. Atualiza os dados do UI
    /// </summary>
    public class StageSelectEntry : MonoBehaviour
    {
        public event Action<StageData> OnPicked;

        public Text label;
        public Button button;
        public GameObject lockedIndicator;
        public GameObject completedIndicator;
        public Text best;

        StageData data;

        public void SetUp(StageData data, int index)
        {
            this.data = data;
            completedIndicator.SetActive(data.Completed);
            lockedIndicator.SetActive(data.Locked);
            label.text = $"{index + 1}";
            button.interactable = !data.Locked;
            best.text = $"Best: {data.BestScore}";
        }

        public void Pick()
        {
            OnPicked?.Invoke(data);
        }
    }
}
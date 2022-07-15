using System.Collections.Generic;
using UnityEngine;

using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Exibição da barra de runas
    /// </summary>
    public class RuneCombinationUI : MonoBehaviour
    {
        public RuneUI runePrefab;
        public List<RectTransform> slots;

        /// <summary>
        /// Adiciona os UI de runas em cada slot de acordo com a combinação
        /// </summary>
        public void SetUp(RuneDefinition[] combination)
        {
            if (combination.Length != slots.Count)
            {
                Debug.LogError($"Falha no combination ui! combination {combination.Length} !=  slots {slots.Count}");
                return;
            }

            for (int i = 0; i < combination.Length; i++)
            {
                //Se houver runa no slot i, instancia o prefab
                if (combination[i] != null)
                {
                    var rune = Instantiate(runePrefab, slots[i]);
                    rune.SetUp(combination[i]);
                }
            }
        }
    }
}
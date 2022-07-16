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
        public List<RuneUI> runes;

        /// <summary>
        /// Adiciona os UI de runas em cada slot de acordo com a combinação
        /// </summary>
        public void SetUp(RuneDefinition[] combination)
        {
            if (combination.Length != runes.Count)
            {
                Debug.LogError($"Falha no combination ui! combination {combination.Length} !=  slots {runes.Count}");
                return;
            }

            for (int i = 0; i < combination.Length; i++)
            {
                var rune = runes[i];
                //Se houver runa na posição, ativa e setup
                if (combination[i] != null)
                {
                    rune.gameObject.SetActive(true);
                    rune.SetUp(combination[i]);
                }
                //Se não, desativa a runa no slot
                else
                {
                    rune.gameObject.SetActive(false);
                }
            }
        }
    }
}
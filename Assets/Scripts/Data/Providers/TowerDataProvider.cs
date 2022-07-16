using UnityEngine;
using Yaw.Game;

namespace Yaw.Data
{
    /// <summary>
    /// Gerencia os dados do summon
    /// </summary>
    public class TowerDataProvider : MonoBehaviour, ISingleDataProvider<TowerData>
    {
        [field: SerializeField]
        public TowerData Data { get; set; }

        public void Set(TowerData value)
        {
            Data = value;
            //TODO deveria ter uma classe que cuida disso

            //Atribui o time certo
            foreach (var teamComp in GetComponentsInChildren<ITeamEntity>())
            {
                teamComp.Team = Data.Team;
            }
        }

        /// <summary>
        /// Altera a vida da torre
        /// </summary>
        public void DecreaseHealth(int damage)
        {
            var d = Data;
            d.Health -= damage;
            Data = d;
        }
    }
}
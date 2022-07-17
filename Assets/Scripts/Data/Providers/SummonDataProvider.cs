using UnityEngine;
using Yaw.Game;

namespace Yaw.Data
{
    /// <summary>
    /// Gerencia os dados do summon
    /// </summary>
    public class SummonDataProvider : MonoBehaviour, ISingleDataProvider<SummonData>
    {
        public SummonData Data { get; set; }

        public void Set(SummonData value)
        {
            Data = value;
            //TODO deveria ter uma classe que cuida disso

            //Atribui o time certo
            foreach (var teamComp in GetComponentsInChildren<ITeamEntity>())
            {
                teamComp.Team = value.Team;
            }

            //Atribui o ataque certo
            foreach (var attackComp in GetComponentsInChildren<IAttacker>())
            {
                attackComp.Damage = value.Attack;
            }
        }

        /// <summary>
        /// Altera a vida do summon
        /// </summary>
        public void DecreaseHealth(int damage)
        {
            var d = Data;
            d.Health -= damage;
            Set(d);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Yaw.Game
{
    public class TakesDamageImpl : MonoBehaviour, ITakesDamage
    {
        [field: SerializeField]
        public int Team { get; set; }
        public bool allowFriendlyFire = false;

        public UnityEvent<int> OnTakeDamage;
        public UnityEvent<int, ITeamEntity> OnTakeDamageDetails;

        public void TakeDamage(int damage, ITeamEntity attacker)
        {
            Debug.Log("Taking damage");
            if (!allowFriendlyFire && attacker.Team == Team)
            {
                Debug.Log("Ignored");
                return;
            }

            OnTakeDamage?.Invoke(damage);
            OnTakeDamageDetails?.Invoke(damage, attacker);
        }
    }
}
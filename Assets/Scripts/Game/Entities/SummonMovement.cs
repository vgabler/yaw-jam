using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Movimenta um summon
    /// </summary>
    public class SummonMovement : MonoBehaviour
    {
        float direction = 1;

        ISingleDataProvider<SummonData> provider;
        Summon entity;

        //TODO dependency injection
        void Start()
        {
            entity = GetComponentInParent<Summon>();
            provider = GetComponentInParent<ISingleDataProvider<SummonData>>();
            //TODO reactive
            direction = provider.Get().team % 2 == 0 ? 1 : -1;
        }

        /// <summary>
        /// Movimenta um summon
        /// </summary>
        void Update()
        {
            if (entity.State != SummonState.Walking)
            {
                return;
            }

            transform.Translate(Time.deltaTime * direction * provider.Get().Speed * Vector3.right);
        }
    }
}

using UnityEngine;
using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Movimentação simples do summon
    /// </summary>
    public class SummonMovement : MonoBehaviour
    {
        float direction = 1;

        ISingleDataProvider<SummonData> provider;
        ISummonStateController stateController;

        //TODO dependency injection
        void Start()
        {
            stateController = GetComponentInParent<ISummonStateController>();
            provider = GetComponentInParent<ISingleDataProvider<SummonData>>();
            //TODO reactive
            direction = provider.Data.team % 2 == 0 ? 1 : -1;
        }

        /// <summary>
        /// Movimenta o summon para frente, de acordo com o time do mesmo
        /// </summary>
        void Update()
        {
            if (stateController.State != SummonState.Walking)
            {
                return;
            }

            transform.Translate(Time.deltaTime * direction * provider.Data.Speed * Vector3.right);
        }
    }
}

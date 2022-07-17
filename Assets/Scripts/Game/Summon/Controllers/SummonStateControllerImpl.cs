using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Gerencia o estado do Summon
    /// </summary>
    public class SummonStateControllerImpl : MonoBehaviour, ISummonStateController
    {
        public SummonState State { get; private set; }

        ISingleDataProvider<SummonData> dataProvider;

        bool waiting;

        public UnityEvent OnDeath;

        private void Start()
        {
            dataProvider = GetComponentInParent<ISingleDataProvider<SummonData>>();
            StartCoroutine(WaitThenGo());
        }

        public bool TryChangeState(SummonState state)
        {
            //TODO regras de mudança de estado
            State = state;
            switch (State)
            {
                //Normalmente chamado pelo AnimatorController, significa que a animação de morte acabou
                case SummonState.Dead:
                    Destroy(gameObject);
                    break;
            }

            return true;
        }

        /// <summary>
        /// Faz verificações para alterar o estado
        /// </summary>
        private void Update()
        {
            switch (State)
            {
                case SummonState.Walking:
                    break;
                case SummonState.Idle:
                    if (!waiting)
                    {
                        State = SummonState.Walking;
                    }
                    break;
                default:
                    return;
            }

            //Se a vida chegar a zero, ativa o estado "dying"
            if (dataProvider.Data.Health <= 0)
            {
                State = SummonState.Dying;
                OnDeath?.Invoke();
            }
        }

        /// <summary>
        /// Espera um pouco antes de sair andando
        /// </summary>
        IEnumerator WaitThenGo()
        {
            waiting = true;
            yield return new WaitForSeconds(1);
            waiting = false;
        }
    }
}
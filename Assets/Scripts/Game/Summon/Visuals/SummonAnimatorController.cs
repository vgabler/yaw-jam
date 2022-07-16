using UnityEngine;
using Yaw.Utils;

namespace Yaw.Game
{
    /// <summary>
    /// Gerencia as animações do summon
    /// </summary>
    public class SummonAnimatorController : MonoBehaviour
    {
        ISummonStateController stateController;
        Animator anim;

        void Start()
        {
            //Buscar as dependências
            anim = GetComponentInParent<Animator>();
            GetComponentInParent<AnimatorEvents>().OnAnimationFinishedEvent += OnAnimationFinished;
            stateController = GetComponentInParent<ISummonStateController>();
        }

        private void OnDestroy()
        {
            var evs = GetComponentInParent<AnimatorEvents>();
            if (evs != null)
            {
                evs.OnAnimationFinishedEvent -= OnAnimationFinished;
            }
        }

        /// <summary>
        /// Quando termina as animações de ataque e de morte, tenta atualizar o estado
        /// </summary>
        private void OnAnimationFinished()
        {
            switch (stateController.State)
            {
                case SummonState.Attacking:
                    stateController.TryChangeState(SummonState.Idle);
                    break;
                case SummonState.Dying:
                    stateController.TryChangeState(SummonState.Dead);
                    break;
            }
        }

        SummonState old;

        /// <summary>
        /// Atualiza os parametros do animator
        /// </summary>
        void Update()
        {
            //TODO reactive

            //Se houve alteração, trigger
            if (old != stateController.State)
            {
                switch (stateController.State)
                {
                    case SummonState.Attacking:
                        anim.SetTrigger("Attack");
                        break;
                    case SummonState.Dying:
                        anim.SetTrigger("Die");
                        break;
                }
            }

            anim.SetBool("Walking", stateController.State == SummonState.Walking);
            old = stateController.State;
        }
    }
}
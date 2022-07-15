using UnityEngine;
using Yaw.Utils;

namespace Yaw.Game
{
    public class SummonAnimatorController : MonoBehaviour
    {
        Summon entity;
        Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            GetComponent<AnimatorEvents>().OnAnimationFinishedEvent += OnAnimationFinished;
            entity = GetComponent<Summon>();
        }

        private void OnDestroy()
        {
            GetComponent<AnimatorEvents>().OnAnimationFinishedEvent -= OnAnimationFinished;
        }

        private void OnAnimationFinished()
        {
            switch (entity.State)
            {
                case SummonState.Attacking:
                    entity.TryChangeState(SummonState.Idle);
                    break;
                case SummonState.Dying:
                    entity.TryChangeState(SummonState.Dead);
                    break;
            }
        }

        SummonState old;

        void Update()
        {
            //TODO reactive

            //Se houve alteração, trigger
            if (old != entity.State)
            {
                switch (entity.State)
                {
                    case SummonState.Attacking:
                        anim.SetTrigger("Attack");
                        break;
                    case SummonState.Dying:
                        anim.SetTrigger("Die");
                        break;
                }
            }

            anim.SetBool("Walking", entity.State == SummonState.Walking);
            old = entity.State;
        }
    }
}
using System.Collections;
using UnityEngine;
using Yaw.Data;
using Yaw.Utils;

namespace Yaw.Game
{
    public enum SummonState { Idle, Walking, Attacking, Dying }

    /// <summary>
    /// Gerencia o estado do Summon
    /// </summary>
    public class Summon : MonoBehaviour, ISingleDataProvider<SummonData>
    {
        public SummonState State { get; private set; }

        [SerializeField]
        SummonData data = new SummonData { Health = 5, Attack = 2, team = 0, Speed = 1 };

        public SummonData Get() => data;

        public void SetUp(SummonData data)
        {
            this.data = data;
            GetComponent<AnimatorEvents>().OnAnimationFinishedEvent += OnAnimationFinished;
            StartCoroutine(WaitThenGo());
        }

        private void OnDestroy()
        {
            GetComponent<AnimatorEvents>().OnAnimationFinishedEvent -= OnAnimationFinished;
        }

        private void OnAnimationFinished()
        {
            switch (State)
            {
                case SummonState.Attacking:
                    State = SummonState.Walking;
                    break;
                case SummonState.Dying:
                    Destroy(gameObject);
                    break;
            }
        }

        //TODO Somente para testes, remover
        IEnumerator WaitThenGo()
        {
            yield return new WaitForSeconds(2);
            State = SummonState.Walking;
            while (data.Health > 0)
            {
                //Se estiver atacando, espera a animação acabar
                if (State == SummonState.Attacking)
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }
                else
                {
                    yield return new WaitForSeconds(1);
                }
                State = data.Health % 2 != 0 ? SummonState.Attacking : SummonState.Walking;
                data.Health--;
            }

            State = SummonState.Dying;
        }
    }
}
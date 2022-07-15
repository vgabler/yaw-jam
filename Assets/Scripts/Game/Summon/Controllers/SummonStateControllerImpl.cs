using System.Collections;
using UnityEngine;
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

        //TODO Somente para testes, remover
        IEnumerator WaitThenGo()
        {
            yield return new WaitForSeconds(2);
            State = SummonState.Walking;
            while (dataProvider.Data.Health > 0)
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
                State = dataProvider.Data.Health % 2 != 0 ? SummonState.Attacking : SummonState.Walking;

                var d = dataProvider.Data;
                d.Health--;
                dataProvider.Set(d);
            }

            State = SummonState.Dying;
        }
    }
}
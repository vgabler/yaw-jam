using UnityEngine;

namespace Yaw.Game
{
    public class SummonTargetAcquire : MonoBehaviour
    {
        public GameObject iAttacker;
        public GameObject iStateController;

        public Transform rayCastOrigin;
        public LayerMask checkMask;

        ISummonStateController stateController;
        IAttacker attacker;

        float Direction => attacker.Team % 2 == 0 ? 1 : -1;

        void Start()
        {
            stateController = iStateController.GetComponent<ISummonStateController>();
            attacker = iAttacker.GetComponent<IAttacker>();
        }

        private void Update()
        {
            //Se estiver parado ou andando, verifica se tem alvo à frente
            switch (stateController.State)
            {
                case SummonState.Idle:
                case SummonState.Walking:
                    var hits = Physics2D.RaycastAll(rayCastOrigin.position, Direction * Vector2.right, attacker.Range * .8f);

                    foreach (var target in hits)
                    {
                        var td = target.transform.GetComponentInParent<ITakesDamage>();

                        //Se não tem componente "ITakesDamage", ou for do mesmo time, ou for invulneravel, ignora
                        if (td == null || td.Team == attacker.Team || td.Invulnerable)
                        {
                            continue;
                        }

                        //Tem alguém no alcance, tenta alternar para modo ataque
                        stateController.TryChangeState(SummonState.Attacking);
                        return;
                    }
                    break;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (rayCastOrigin == null || attacker == null)
            {
                return;
            }

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(rayCastOrigin.position, (Vector2)rayCastOrigin.position + Direction * attacker.Range * .8f * Vector2.right);
        }
    }
}
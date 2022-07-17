using UnityEngine;

namespace Yaw.Game
{
    public class MeleeDamager : MonoBehaviour, IAttacker
    {
        [field: SerializeField]
        public int Team { get; set; }
        [field: SerializeField]
        public int Damage { get; set; } = 1;
        [field: SerializeField]
        public float Range { get; set; } = 1;

        public Transform attackOrigin;

        [Tooltip("Se o ataque causa dano para todos os inimigos no alcance")]
        public bool areaDamage = true;
        public LayerMask checkLayer;

        /// <summary>
        /// Verifica se existem inimigos no alcance, e causa dano
        /// Normalmente chamado por outra classe, como evento de animação
        /// </summary>
        public void Attack()
        {
            //Faz uma busca de colisão
            var hits = Physics2D.CircleCastAll(attackOrigin.position, Range, Vector2.zero, 0, checkLayer);

            //Se não tinha ninguém, ignora
            if (hits.Length < 1)
            {
                return;
            }

            foreach (var target in hits)
            {
                var td = target.transform.GetComponentInParent<ITakesDamage>();

                //Se não tem componente "ITakesDamage", ignora
                if (td == null || td.Team == Team || td.Invulnerable)
                {
                    continue;
                }

                td.TakeDamage(Damage, this);

                //Se não for dano em área, atinge só o primeiro e ignora o resto
                if (!areaDamage)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Para facilitar a definição do ataque
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            if (attackOrigin == null)
            {
                return;
            }

            Gizmos.DrawWireSphere(attackOrigin.position, Range);
        }
    }
}
using Code.Classes.CombatController;
using Code.Classes.MovementController;
using System.Collections;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class Clown : BossEnemy
    {
        public override void HitByProjectile(GameObject projectile)
        {
            base.HitByProjectile(projectile);
            CombatController.ReceiveHit(new Collision2D());
        }

        public override void StartBossFight()
        {
        }

        protected override IEnumerator Patrol()
        {
            while (true)
            {
                MovementController.Move(1);
                yield return new WaitForSeconds(1);
                MovementController.Move(-1);
                yield return new WaitForSeconds(1);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Contains("Weapon"))
                CombatController.ReceiveHit(collision);
        }

        private void Start()
        {
            CombatController = new EnemyCombatController(gameObject, 6);
            MovementController = new PatrollingEnemyMovementController(gameObject, WalkingSpeed);
        }
    }
}
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
            CombatController.ReceiveHit();
        }

        public override void StartBossFight()
        {
        }

        protected override void Start()
        {
            CombatController = new EnemyCombatController(gameObject, 6);
            MovementController = new PatrollingEnemyMovementController(gameObject, WalkingSpeed);
        }
    }
}
using System.Collections;
using Code.Classes.MovementController;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public abstract class BasePatrolingEnemy : BaseEntity
    {
        public bool Patrolling;

        public void StartPatrolling()
        {
            StartCoroutine(Patrol());
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (NotHitable(collision))
                return;
            StartCoroutine(BrieflyTurnInvincibleAndBlink());
            CombatController.ReceiveHit(collision.collider);
        }

        protected virtual IEnumerator Patrol()
        {
            while (true)
            {
                MovementController.Move(1);
                yield return new WaitForSeconds(1);
                MovementController.Move(-1);
                yield return new WaitForSeconds(1);
            }
        }

        protected virtual void Start()
        {
            WalkingSpeed = 2;
            MovementController = new PatrollingEnemyMovementController(gameObject, WalkingSpeed);
            if (Patrolling)
                StartPatrolling();
        }
        private bool NotHitable(Collision2D collision)
        {
            return !collision.gameObject.tag.Contains("Weapon") && !invincible;
        }
    }
}
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
            DealWithCollision(collision.gameObject);
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            DealWithCollision(other.gameObject);
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

        private void DealWithCollision(GameObject otherGameObject)
        {
            if (NotHitable(otherGameObject))
                return;
            StartCoroutine(BrieflyTurnInvincibleAndBlink());
            CombatController.ReceiveHit();
        }
        private bool NotHitable(GameObject otherGameObject)
        {
            return !otherGameObject.tag.Contains("Weapon") || Invincible;
        }
    }
}
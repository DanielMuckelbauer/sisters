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

        protected virtual void Start()
        {
            WalkingSpeed = 2;
            MovementController = new PatrollingEnemyMovementController(gameObject, WalkingSpeed);
            if (Patrolling)
                StartPatrolling();
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

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.tag.Contains("Weapon"))
                return;
            CombatController.ReceiveHit(collision);
        }
    }
}
﻿using System.Collections;
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

        protected IEnumerator JumpRandomly()
        {
            while (true)
            {
                yield return new WaitForSeconds(2 + Random.value * 3);
                MovementController.Jump();
            }
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            DealWithCollision(collision.gameObject);
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            DealWithCollision(other.gameObject);
        }

        protected virtual IEnumerator Patrol(int interval = 1)
        {
            while (true)
            {
                MovementController.Move(1);
                yield return new WaitForSeconds(interval);
                MovementController.Move(-1);
                yield return new WaitForSeconds(interval);
            }
        }

        protected virtual void Start()
        {
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
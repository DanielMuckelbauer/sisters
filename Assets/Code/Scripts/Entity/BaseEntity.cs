using System;
using System.Collections;
using Code.Classes.CombatController;
using Code.Classes.MovementController;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public abstract class BaseEntity : MonoBehaviour
    {
        public Animator Animator;
        protected float WalkingSpeed = 5;
        protected IMovementController MovementController;
        protected ICombatController CombatController;

        public virtual void HitByProjectile(GameObject projectile)
        {
            Destroy(projectile);
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }

        public void GoTo(Vector3 target, float tolerance)
        {
            StartCoroutine(MoveToTarget(target, tolerance));
        }

        private IEnumerator MoveToTarget(Vector3 target, float tolerance)
        {
            int horizontal = (target.x < transform.position.x) ? -1 : 1;
            Vector3 currentPosition = transform.position;
            while (Vector3.Distance(currentPosition, target) > tolerance)
            {
                Debug.Log(Vector3.Distance(transform.position, target));
                MovementController.Move(horizontal);
                currentPosition = transform.position;
                yield return null;
            }

            MovementController.Move(0);
        }
    }
}
using Code.Classes.CombatController;
using Code.Classes.MovementController;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public abstract class BaseEntity : MonoBehaviour
    {
        public Animator Animator;
        protected ICombatController CombatController;
        protected IMovementController MovementController;
        protected float WalkingSpeed = 5;
        protected bool Invincible;

        public virtual void Die()
        {
            Destroy(gameObject);
        }

        public void GoTo(Vector3 target, float tolerance)
        {
            StartCoroutine(MoveToTarget(target, tolerance));
        }

        public virtual void HitByProjectile(GameObject projectile)
        {
            Destroy(projectile);
        }

        protected IEnumerator BrieflyTurnInvincibleAndBlink()
        {
            Invincible = true;
            yield return StartCoroutine(Blink(10));
            Invincible = false;
        }

        private IEnumerator Blink(int times)
        {
            List<SpriteRenderer> allRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
            for (int i = 0; i < times; i++)
            {
                float opacity = i % 2 == 0 ? 0.5f : 1;
                allRenderers.ForEach(r => r.color = new Color(1f, 1f, 1f, opacity));
                yield return new WaitForSeconds(0.1f);
            }
        }

        private IEnumerator MoveToTarget(Vector3 target, float tolerance)
        {
            int horizontal = (target.x < transform.position.x) ? -1 : 1;
            Vector3 currentPosition = transform.position;
            while (Vector3.Distance(currentPosition, target) > tolerance)
            {
                MovementController.Move(horizontal);
                currentPosition = transform.position;
                yield return null;
            }

            MovementController.Move(0);
        }
    }
}
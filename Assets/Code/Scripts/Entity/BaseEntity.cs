using Code.Classes.CombatController;
using Code.Classes.MovementController;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public abstract class BaseEntity : MonoBehaviour
    {
        public Animator Animator;
        protected ICombatController CombatController;
        protected bool Invincible;
        protected IMovementController MovementController;
        protected float WalkingSpeed = 5;

        public virtual void Die()
        {
            Destroy(gameObject);
        }

        public void GoTo(Vector3 target)
        {
            StartCoroutine(MoveToTarget(target));
        }

        public virtual void HitByProjectile(GameObject projectile)
        {
            Destroy(projectile);
        }

        protected IEnumerator BrieflyTurnInvincibleAndBlink()
        {
            Invincible = true;
            yield return StartCoroutine(Blink(7));
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

        private IEnumerator MoveToTarget(Vector3 target)
        {
            int horizontal = target.x < transform.position.x ? -1 : 1;
            bool targetReached = false;
            while (!targetReached)
            {
                MovementController.Move(horizontal);
                if (horizontal == 1)
                    targetReached = transform.position.x > target.x;
                else
                    targetReached = transform.position.x < target.x;
                yield return null;
            }

            Debug.Log(gameObject.name + "Target reached");
            MovementController.Move(0);
        }
    }
}
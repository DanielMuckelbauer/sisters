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
        [SerializeField] protected int MaxLife = 1;
        protected IMovementController MovementController;
        [SerializeField] protected float WalkingSpeed = 5;
        protected bool Invincible;

        public virtual void Die()
        {
            Destroy(gameObject);
        }

        public IEnumerator GoTo(Vector3 target)
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

            MovementController.Move(0);
        }

        public virtual void HitByProjectile(GameObject projectile)
        {
            Destroy(projectile);
        }

        public IEnumerator TurnTo(Vector3 target)
        {
            int horizontal = target.x < transform.position.x ? -1 : 1;
            MovementController.Move(horizontal);
            yield return null;
            MovementController.Move(0);
        }

        protected IEnumerator BrieflyTurnInvincibleAndBlink()
        {
            Invincible = true;
            yield return StartCoroutine(Blink(8));
            Invincible = false;
        }

        private static float NextBlinkOpacity(int i)
        {
            return i % 2 == 0 ? 0.5f : 1;
        }

        private IEnumerator Blink(int times)
        {
            if (times % 2 != 0)
                times += 1;
            List<SpriteRenderer> allRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
            for (int i = 0; i < times; i++)
            {
                float opacity = NextBlinkOpacity(i);
                allRenderers.ForEach(r => r.color = new Color(1f, 1f, 1f, opacity));
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
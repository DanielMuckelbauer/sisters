using System;
using System.Collections;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class Clown : BasePatrollingEnemy
    {
        public event Action OnDestroyed;

        public override void HitByProjectile(GameObject projectile)
        {
            base.HitByProjectile(projectile);
            CombatController.ReceiveHit();
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
            OnDestroyed = null;
        }

        public void StartPunching()
        {
            StartCoroutine(PunchingLoop());
        }

        private IEnumerator PunchingLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(6);
                GetComponent<Animator>().SetTrigger("Punch");
            }
        }

        public void ResetOnDestroy()
        {
            OnDestroyed = null;
        }
    }
}
using System;
using System.Collections;
using Code.Classes.MovementController;
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
        }

        public void StartPunching()
        {
            StartCoroutine(PunchingLoop());
        }

        private IEnumerator PunchingLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(7);
                GetComponent<Animator>().SetTrigger("Punch");
            }
        }
    }
}
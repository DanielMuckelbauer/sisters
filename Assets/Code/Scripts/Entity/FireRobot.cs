using System.Collections;
using Code.Classes.CombatController;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class FireRobot : BasePatrollingEnemy
    {
        public AudioSource AudioSource;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(ShootPeriodically());
        }

        private IEnumerator ShootPeriodically()
        {
            while (true)
            {
                yield return new WaitForSeconds(7);
                AudioSource.Play();
                Animator.SetTrigger("Shoot");
            }
        }
    }
}
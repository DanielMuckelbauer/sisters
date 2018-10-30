using System.Collections;
using Code.Classes.CombatController;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class FireRobot : BasePatrolingEnemy
    {
        protected override void Start()
        {
            base.Start();
            CombatController = new EnemyCombatController(gameObject, 3);
            StartCoroutine(ShootPeriodically());
        }

        private IEnumerator ShootPeriodically()
        {
            while (true)
            {
                yield return new WaitForSeconds(7);
                Animator.SetTrigger("Shoot");
            }
        }
    }
}
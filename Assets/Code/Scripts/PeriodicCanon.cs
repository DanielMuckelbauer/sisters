using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
    public class PeriodicCanon : BaseCanon
    {
        public Transform Target;

        private void Start()
        {
            StartCoroutine(ShootPeriodically());
        }

        private IEnumerator ShootPeriodically()
        {
            while (true)
            {
                yield return new WaitForSeconds(2);
                ShootProjectile(Target.position);
            }
        }
    }
}
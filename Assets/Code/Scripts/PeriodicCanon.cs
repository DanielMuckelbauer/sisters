using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
    public class PeriodicCanon : BaseCanon
    {
        public Transform Target;

        void Start()
        {
            StartCoroutine(ShootPeriodically());
        }

        private IEnumerator ShootPeriodically()
        {
            while (true)
            {
                yield return new WaitForSeconds(4);
                ShootProjectile(Target.position);
            }
        }
    }
}
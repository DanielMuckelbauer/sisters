using System.Collections;
using UnityEngine;

namespace Code.Scripts.Level
{
    public class PeriodicCanon : BaseCanon
    {
        public Transform Target;
        public AudioSource AudioSource;

        private void Start()
        {
            StartCoroutine(ShootPeriodically());
        }

        private IEnumerator ShootPeriodically()
        {
            while (true)
            {
                yield return new WaitForSeconds(2);
                AudioSource.Play();
                ShootProjectile(Target.position);
            }
        }
    }
}
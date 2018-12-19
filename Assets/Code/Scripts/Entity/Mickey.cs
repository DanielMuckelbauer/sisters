using System.Collections;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class Mickey : BasePatrollingEnemy
    {
        [SerializeField] private Transform source1;
        [SerializeField] private Transform source2;
        [SerializeField] private Transform target1;
        [SerializeField] private Transform target2;
        [SerializeField] private GameObject bullet;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(ShootPeriodically());
        }

        private IEnumerator ShootPeriodically()
        {
            while (true)
            {
                yield return new WaitForSeconds(2);
                InstantiateAndShootProjectile(bullet, source1, target1);
                yield return new WaitForSeconds(2);
                InstantiateAndShootProjectile(bullet, source2, target2);
            }
        }
    }
}
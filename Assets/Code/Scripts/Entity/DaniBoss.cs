using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Code.Scripts.Entity
{
    public class DaniBoss : BasePatrolingEnemy
    {
        [SerializeField] private GameObject EnergyBall;
        [SerializeField] private GameObject Muni;
        [SerializeField] private GameObject Pollin;
        [SerializeField] private Transform LeftSource;
        private GameObject currentTarget;

        protected override void Start()
        {
            base.Start();
            currentTarget = Muni;
            StartCoroutine(StartAttackLoop());
        }

        private IEnumerator StartAttackLoop()
        {
            while (true)
            {
                Debug.Log("Shoot");
                currentTarget = currentTarget == Muni ? Pollin : Muni;
                Animator.SetTrigger("ShootLeft");
                yield return new WaitForSeconds(5);
            }
        }

        public void ShootLeft()
        {
            InstantiateAndShootProjectile(EnergyBall, LeftSource, currentTarget.transform);
        }
    }
}
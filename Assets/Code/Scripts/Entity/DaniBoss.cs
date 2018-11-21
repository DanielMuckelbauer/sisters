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

        public void ShootLeft()
        {
            Debug.Log("Shot Left");
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(StartAttackLoop());
        }

        private IEnumerator StartAttackLoop()
        {
            GameObject currentTarget = Muni;
            while (true)
            {
                currentTarget = currentTarget == Muni ? Pollin : Muni;
                InstantiateAndShootProjectile(EnergyBall, LeftSource, currentTarget.transform);
            }
        }
    }
}
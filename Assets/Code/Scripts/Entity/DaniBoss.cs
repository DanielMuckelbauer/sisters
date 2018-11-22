using System.Collections;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class DaniBoss : BasePatrolingEnemy
    {
        private GameObject currentTarget;
        [SerializeField] private GameObject energyBall;
        [SerializeField] private Transform leftSource;
        [SerializeField] private GameObject muni;
        [SerializeField] private GameObject pollin;
        private bool targetIsLeft;
        public void Shoot()
        {
            InstantiateAndShootProjectile(energyBall, leftSource, currentTarget.transform);
        }

        protected override void Start()
        {
            base.Start();
            currentTarget = muni;
            StartCoroutine(StartAttackLoop());
        }

        private void SetTargetLeft()
        {
            targetIsLeft = currentTarget.transform.position.x < transform.position.x;
            Animator.SetBool("TargetIsLeft", targetIsLeft);
        }

        private IEnumerator StartAttackLoop()
        {
            while (true)
            {
                currentTarget = currentTarget == muni ? pollin : muni;
                SetTargetLeft();
                Animator.SetTrigger("Shoot");
                yield return new WaitForSeconds(5);
            }
        }
    }
}
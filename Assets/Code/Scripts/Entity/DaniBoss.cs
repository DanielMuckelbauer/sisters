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
        [SerializeField] private Transform rightSource;
        private bool targetIsLeft;

        public void Shoot()
        {
            Transform source = targetIsLeft ? leftSource : rightSource;
            InstantiateAndShootProjectile(energyBall, source, currentTarget.transform);
        }

        public void StartFight()
        {
            StartCoroutine(FlyUpAndStartFighting());
        }

        protected override void Start()
        {
            base.Start();
            currentTarget = muni;
        }
        private IEnumerator FlyUpAndStartFighting()
        {
            Animator.SetTrigger("FlyUp");
            yield return new WaitForSeconds(4);
            StartCoroutine(StartAttackLoop());
            StartPatrolling();
        }

        private void SetTargetPosition()
        {
            targetIsLeft = currentTarget.transform.position.x < transform.position.x;
            Animator.SetBool("TargetIsLeft", targetIsLeft);
        }

        private IEnumerator StartAttackLoop()
        {
            while (true)
            {
                currentTarget = currentTarget == muni ? pollin : muni;
                SetTargetPosition();
                Animator.SetTrigger("Shoot");
                yield return new WaitForSeconds(5);
            }
        }
    }
}
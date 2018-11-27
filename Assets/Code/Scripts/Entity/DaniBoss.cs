using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class DaniBoss : BasePatrolingEnemy
    {
        private GameObject currentTarget;
        [SerializeField] private GameObject energyBall;
        private Coroutine energyBallShootLoop;
        [SerializeField] private Stack<int> healthBorders;
        private Coroutine laserShootLoop;
        [SerializeField] private Transform leftSource;
        [SerializeField] private GameObject muni;
        [SerializeField] private GameObject pollin;
        [SerializeField] private Transform rightSource;
        private bool targetIsLeft;
        public event Action OnNextPhase;

        public void Shoot()
        {
            Transform source = targetIsLeft ? leftSource : rightSource;
            InstantiateAndShootProjectile(energyBall, source, currentTarget.transform);
        }

        public void StartFight()
        {
            StartCoroutine(FlyUpAndStartFighting());
        }

        public void StartShooting()
        {
            energyBallShootLoop = StartCoroutine(StartAttackLoop());
        }

        public void StartShootingLaser()
        {
            laserShootLoop = StartCoroutine(StartLaserLoop());
        }

        private IEnumerator StartLaserLoop()
        {
            while (true)
            {
                Animator.SetTrigger("ShootLaser");
                yield return new WaitForSeconds(6);
            }
        }

        protected override void DealWithCollision(GameObject other)
        {
            base.DealWithCollision(other);
            StartNextPhaseIfNecessary();
        }

        protected override IEnumerator Patrol(int interval = 1)
        {
            Vector3 startPos = transform.position;
            float xScale = 2;
            float yScale = 1;
            while (true)
            {
                transform.position =
                    startPos + (Vector3.right * Mathf.Sin(Time.timeSinceLevelLoad / 2 * WalkingSpeed) * xScale -
                                Vector3.up * Mathf.Sin(Time.timeSinceLevelLoad * WalkingSpeed) * yScale);
                yield return null;
            }
        }

        protected override void Start()
        {
            base.Start();
            currentTarget = muni;
            InitializeHealthStack();
        }

        private IEnumerator FlyUpAndStartFighting()
        {
            Animator.SetTrigger("FlyUp");
            yield return new WaitForSeconds(4);
            StartShooting();
            StartPatrolling();
        }
        private void InitializeHealthStack()
        {
            healthBorders = new Stack<int>();
            //TODO Change in Prod
            //healthBorders.Push(5);
            //healthBorders.Push(10);
            //healthBorders.Push(15);
            healthBorders.Push(17);
            healthBorders.Push(18);
            healthBorders.Push(19);
        }

        private void SetTargetDirection()
        {
            targetIsLeft = currentTarget.transform.position.x < transform.position.x;
            Animator.SetBool("TargetIsLeft", targetIsLeft);
        }

        private IEnumerator StartAttackLoop()
        {
            while (true)
            {
                currentTarget = currentTarget == muni ? pollin : muni;
                SetTargetDirection();
                Animator.SetTrigger("Shoot");
                yield return new WaitForSeconds(4);
            }
        }

        private void StartNextPhaseIfNecessary()
        {
            if (CombatController.CurrentLife > healthBorders.Peek())
                return;
            StopShooting();
            healthBorders.Pop();
            OnNextPhase?.Invoke();
        }
        private void StopShooting()
        {
            StopCoroutine(energyBallShootLoop);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class DaniBoss : BasePatrollingEnemy
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
            if (currentTarget == null)
                return;
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
            //healthBorders.Push(1);
            //healthBorders.Push(12);
            //healthBorders.Push(18);
            healthBorders.Push(23);
            healthBorders.Push(24);
            healthBorders.Push(25);
        }

        private void SetTargetDirection()
        {
            if (currentTarget == null)
                return;
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
                yield return new WaitForSeconds(2.5f);
            }
        }

        private IEnumerator StartLaserLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(4);
                Animator.SetTrigger("ShootLaser");
            }
        }

        private void StartNextPhaseIfNecessary()
        {
            if (healthBorders.Count < 1 || CombatController.CurrentLife > healthBorders.Peek())
                return;
            StopShooting();
            healthBorders.Pop();
            OnNextPhase?.Invoke();
        }

        private void StopShooting()
        {
            StopCoroutine(energyBallShootLoop);
            if (laserShootLoop != null)
                StopCoroutine(laserShootLoop);
        }
    }
}
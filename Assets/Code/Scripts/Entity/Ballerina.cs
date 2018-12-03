using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class Ballerina : BasePatrollingEnemy
    {
        public Transform LeftShoeSource;
        public List<Transform> LeftTargets;
        public Transform RightShoeSource;
        public List<Transform> RightTargets;
        public GameObject Shoe;
        private Dictionary<Transform, List<Transform>> directions;

        public void StartFighting()
        {
            StartCoroutine(Patrol(2));
            StartCoroutine(JumpRandomly());
            StartCoroutine(ShootLoop());
        }

        protected override void Start()
        {
            base.Start();
            InitializeShootDirections();
            StartFighting();
        }

        private void InitializeShootDirections()
        {
            directions =
                new Dictionary<Transform, List<Transform>>
                {
                    {LeftShoeSource, LeftTargets},
                    {RightShoeSource, RightTargets}
                };
        }

        private IEnumerator ShootLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(4);
                ShootShoes();
            }
        }

        private void ShootShoes()
        {
            foreach (KeyValuePair<Transform, List<Transform>> pair in directions)
            {
                pair.Value.ForEach(target =>
                {
                    InstantiateAndShootProjectile(Shoe, pair.Key, target);
                });
            }
        }
    }
}
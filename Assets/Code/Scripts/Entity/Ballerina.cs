using System;
using System.Collections;
using System.Collections.Generic;
using Code.Classes.CombatController;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class Ballerina : BasePatrolingEnemy
    {
        public Transform LeftShoeSource;
        public Transform RightShoeSource;
        public GameObject Shoe;
        public List<Transform> LeftTargets;
        public List<Transform> RightTargets;

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
            CombatController = new EnemyCombatController(gameObject, 5);
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
                    InstantiateAndShootProjectile(Shoe, target, pair.Key);
                });
            }
        }
    }
}
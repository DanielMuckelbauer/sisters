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

        private const float ShootForce = 5;
        private Dictionary<Transform, List<Vector3>> directions;

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
            WalkingSpeed = 5;
            CombatController = new EnemyCombatController(gameObject, 5);
            StartFighting();
        }

        private void InitializeShootDirections()
        {
            directions = new Dictionary<Transform, List<Vector3>>();
            List<Vector3> leftDirections =
                new List<Vector3> {Vector3.left, Vector3.left + Vector3.down, Vector3.left + Vector3.up};
            List<Vector3> rightDirections =
                new List<Vector3> {Vector3.right, Vector3.right + Vector3.down, Vector3.right + Vector3.up};
            directions.Add(LeftShoeSource, leftDirections);
            directions.Add(RightShoeSource, rightDirections);
        }

        private IEnumerator ShootLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(3);
                ShootShoes();
            }
        }

        private void ShootShoes()
        {
            foreach (KeyValuePair<Transform, List<Vector3>> pair in directions)
            {
                pair.Value.ForEach(v =>
                {
                    Rigidbody2D rigidBody = Instantiate(Shoe, pair.Key).GetComponent<Rigidbody2D>();
                    rigidBody.AddForce(v * ShootForce);
                });
            }
        }
    }
}
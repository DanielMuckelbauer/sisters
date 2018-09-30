using System.Collections;
using Code.Classes.CombatController;
using Code.Classes.MovementController;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class Clown : BaseEnemy
    {
        public GameObject Player;
        public GameObject EnergyBall;
        public Transform EnergyBallSource;

        private void Start()
        {
            CombatController = new EnemyCombatController(gameObject);
            MovementController = new PatrolingEnemyMovementController(gameObject, WalkingSpeed);
            StartCoroutine(Attack());
            //StartPatroling();
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(3);
                GameObject energyBall = Instantiate(EnergyBall, EnergyBallSource.position, new Quaternion());
                Projectile projectile = energyBall.GetComponent<Projectile>();
                projectile.Shoot(Player.transform.position);
            }
        }

        protected override IEnumerator Patrol()
        {
            while (true)
            {
                MovementController.Move(1);
                yield return new WaitForSeconds(1);
                MovementController.Move(-1);
                yield return new WaitForSeconds(1);
            }
        }
    }
}
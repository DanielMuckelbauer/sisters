using System.Collections;
using Code.Classes.CombatController;
using Code.Classes.MovementController;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class Clown : BossEnemy
    {
        public GameObject Player;
        public GameObject EnergyBall;
        public Transform EnergyBallSource;

        private void Start()
        {
            CombatController = new EnemyCombatController(gameObject, 3);
            MovementController = new PatrolingEnemyMovementController(gameObject, WalkingSpeed);
        }

        public override void StartBossFight()
        {
            StartCoroutine(Attack());
        }

        public override void ProjectileEntered(GameObject projectile)
        {
            Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag.Contains("Weapon"))
                CombatController.ReceiveHit(collision);
        }
    }
}
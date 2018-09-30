using System.Collections;
using Code.Classes.CombatController;
using Code.Classes.MovementController;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class Clown : BaseEnemy
    {
        private void Start()
        {
            CombatController = new EnemyCombatController(gameObject);
            MovementController = new PatrolingEnemyMovementController(gameObject, WalkingSpeed);
            StartPatroling();
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


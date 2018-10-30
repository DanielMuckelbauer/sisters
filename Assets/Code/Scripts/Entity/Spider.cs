using Code.Classes.CombatController;
using Code.Classes.MovementController;
using System.Collections;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class Spider : BasePatrolingEnemy
    {
        protected override void Start()
        {
            base.Start();
            CombatController = new EnemyCombatController(gameObject, 1);
            StartCoroutine(JumpRandomly());
        }

        private IEnumerator JumpRandomly()
        {
            while (true)
            {
                yield return new WaitForSeconds(2 + Random.value * 3);
                MovementController.Jump();
            }
        }
    }
}
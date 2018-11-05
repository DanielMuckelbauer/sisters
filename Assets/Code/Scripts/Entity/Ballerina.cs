using System.Collections;
using Code.Classes.CombatController;

namespace Code.Scripts.Entity
{
    public class Ballerina : BasePatrolingEnemy
    {
        public void StartFighting()
        {
            StartCoroutine(Patrol(4));
            StartCoroutine(JumpRandomly());
        }

        protected override void Start()
        {
            base.Start();
            WalkingSpeed = 5;
            CombatController = new EnemyCombatController(gameObject, 5);
            StartFighting();
        }
    }
}
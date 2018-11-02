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
    }
}
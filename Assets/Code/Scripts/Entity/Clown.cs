using System.Collections;
using Code.Classes.CombatController;
using Code.Classes.MovementController;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class Clown : BaseEntity
    {
        private void Start()
        {
            CombatController = new EnemyCombatController(gameObject);
            MovementController = new PatrolingEnemyMovementController(gameObject, WalkingSpeed);
        }
    }
}


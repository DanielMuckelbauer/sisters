using Code.Classes.CombatController;
using Code.Classes.MovementController;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public abstract class BaseEntity : MonoBehaviour
    {
        public Animator Animator;
        protected float WalkingSpeed = 5;
        protected IMovementController MovementController;
        protected ICombatController CombatController;
    }
}
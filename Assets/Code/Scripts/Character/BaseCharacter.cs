using Code.Classes;
using Code.Classes.MovementController;
using UnityEngine;

namespace Code.Scripts.Character
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        protected IMovementController MovementController;
        protected ICombatController CombatController;
    }
}
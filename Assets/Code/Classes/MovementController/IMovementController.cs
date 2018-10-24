using UnityEngine;

namespace Code.Classes.MovementController
{
    public interface IMovementController
    {
        void Move(float horizontal);
        void Jump();
        bool LookingRight { get; set; }
        bool CheckGrounded();
    }
}

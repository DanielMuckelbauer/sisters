using UnityEngine;

namespace Code.Classes.MovementController
{
    public class SpiderMovementController : BaseMovementController
    {
        public SpiderMovementController(GameObject gameObject) : base(gameObject)
        {
            Speed = 2;
        }

        public override void Move(float horizontal)
        {
            Vector2 currentVelocity = Vector2.zero;
            currentVelocity.x = horizontal * Speed;
            RigidBody.velocity = currentVelocity;
        }

        public override void Jump()
        {
            RigidBody.AddForce(Vector2.up * JumpForce);
        }
    }
}
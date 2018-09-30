using UnityEngine;

namespace Code.Classes.MovementController
{
    public class PatrolingEnemyMovementController : BaseMovementController
    {
        public PatrolingEnemyMovementController(GameObject gameObject, float speed) : base(gameObject)
        {
            Speed = speed;
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
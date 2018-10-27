using UnityEngine;

namespace Code.Classes.MovementController
{
    public class PatrollingEnemyMovementController : BaseMovementController
    {
        public PatrollingEnemyMovementController(GameObject gameObject, float speed) : base(gameObject)
        {
            Speed = speed;
        }

        public override void Jump()
        {
            RigidBody.AddForce(Vector2.up * JumpForce);
        }

        public override void Move(float horizontal)
        {
            Vector2 currentVelocity = Vector2.zero;
            currentVelocity.x = horizontal * Speed;
            RigidBody.velocity = currentVelocity;
        }


    }
}
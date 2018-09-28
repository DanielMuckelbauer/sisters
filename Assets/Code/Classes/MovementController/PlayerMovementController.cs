using UnityEngine;

namespace Code.Classes.MovementController
{
    public class PlayerMovementController : BaseMovementController
    {
        private const float Speed = 4;
        private const float JumpForce = 400;

        public PlayerMovementController(GameObject gameObject, Transform groundCheck) : base(gameObject, groundCheck)
        {
        }

        public override void Move(float horizontal)
        {
            ApplyMovement(horizontal);
            TryFlip(horizontal);
        }

        private void ApplyMovement(float horizontal)
        {
            Vector2 currentVelocity = RigidBody.velocity;
            currentVelocity.x = horizontal * Speed;
            RigidBody.velocity = currentVelocity;
        }

        private void TryFlip(float horizontal)
        {
            if (LookingRight && horizontal < 0)
            {
                LookingRight = false;
                Flip();
            }
            else if (!LookingRight && horizontal > 0)
            {
                LookingRight = true;
                Flip();
            }
        }

        private void Flip()
        {
            Vector3 theScale = Transform.localScale;
            theScale.x *= -1;
            Transform.localScale = theScale;
        }

        public override void Jump()
        {
            bool grounded =
                Physics2D.Linecast(Transform.position, GroundCheck.position, LayerMask);
            if (grounded)
                RigidBody.AddForce(Vector2.up * JumpForce);
        }
    }
}
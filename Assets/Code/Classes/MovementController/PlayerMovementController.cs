using UnityEngine;

namespace Code.Classes.MovementController
{
    public class PlayerMovementController : BaseMovementController
    {
        private readonly Transform groundCheck;

        public PlayerMovementController(GameObject gameObject, float speed, Transform groundCheck) : base(gameObject)
        {
            this.groundCheck = groundCheck;
            Speed = speed;
        }


        public override void Move(float horizontal)
        {
            ApplyMovement(horizontal);
            Animator.SetInteger("Walking", (int) horizontal);
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
                Physics2D.Linecast(Transform.position, groundCheck.position, LayerMask);
            if (!grounded) 
                return;
            Animator.SetTrigger("Jump");
            RigidBody.AddForce(Vector2.up * JumpForce);
        }
    }
}
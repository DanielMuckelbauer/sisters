using UnityEngine;

namespace Code.Classes
{
    public class PlayerMovementController : IMovementController
    {
        public bool LookingRight { get; set; }
        public bool Grounded { get; set; }

        private const float Speed = 4;
        private const float JumpForce = 400;
        private readonly Transform transform;
        private readonly Rigidbody2D rigidBody;

        public PlayerMovementController(GameObject player)
        {
            rigidBody = player.GetComponent<Rigidbody2D>();
            transform = player.GetComponent<Transform>();
            LookingRight = true;
        }

        public void Move(float horizontal)
        {
            ApplyMovement(horizontal);
            TryFlip(horizontal);
        }

        private void ApplyMovement(float horizontal)
        {
            Vector2 currentVelocity = rigidBody.velocity;
            currentVelocity.x = horizontal * Speed;
            rigidBody.velocity = currentVelocity;
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
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void Jump()
        {
            if (!Grounded) return;
            rigidBody.AddForce(Vector2.up * JumpForce);
            Grounded = false;
        }
    }
}
using UnityEngine;

namespace Code.Classes
{
    public class PlayerMovementController : IMovementController
    {
        public bool LookingRight { get; set; }
        public bool Grounded { get; set; }

        private const float MoveForce = 10;
        private const float MaxSpeed = 10;
        private const float JumpForce = 350;
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
            if (Mathf.Abs(horizontal) < MaxSpeed)
                rigidBody.AddForce(Vector2.right * horizontal * MoveForce);
            TryFlip(horizontal);
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
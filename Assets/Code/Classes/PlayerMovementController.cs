using UnityEngine;

namespace Code.Classes
{
    public class PlayerMovementController : IMovementController
    {
        public bool LookingRight { get; set; }
        public bool Grounded { get; set; }

        private const float MoveForce = 5;
        private const float MaxSpeed = 10;
        private const float JumpForce = 200;
        private readonly Transform transform;
        private readonly Rigidbody2D rigidBody;
        private readonly SpriteRenderer spriteRenderer;

        public PlayerMovementController(GameObject player)
        {
            rigidBody = player.GetComponent<Rigidbody2D>();
            spriteRenderer = player.GetComponent<SpriteRenderer>();
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
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            else if (!LookingRight && horizontal > 0)
            {
                LookingRight = true;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }

        public void Jump()
        {
            if (Grounded)
                rigidBody.AddForce(Vector2.up * JumpForce);
        }
    }
}
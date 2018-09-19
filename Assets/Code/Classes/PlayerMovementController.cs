using UnityEngine;

namespace Classes
{
    public class PlayerMovementController : IMovementController
    {
        private bool lookingRight = true;
        private const float MoveForce = 5;
        private const float MaxSpeed = 10;
        private readonly Rigidbody2D rigidBody;
        private readonly SpriteRenderer spriteRenderer;

        public PlayerMovementController(GameObject player)
        {
            rigidBody = player.GetComponent<Rigidbody2D>();
            spriteRenderer = player.GetComponent<SpriteRenderer>();
        }

        public void Move(float horizontal)
        {
            if (Mathf.Abs(horizontal) < MaxSpeed)
                rigidBody.AddForce(Vector2.right * horizontal * MoveForce);
            TryFlip(horizontal);
        }

        private void TryFlip(float horizontal)
        {
            if (lookingRight && horizontal < 0)
            {
                lookingRight = false;
                spriteRenderer.flipX = true;
            }
            else if (!lookingRight && horizontal > 0)
            {
                lookingRight = true;
                spriteRenderer.flipX = false;
            }
        }
    }
}
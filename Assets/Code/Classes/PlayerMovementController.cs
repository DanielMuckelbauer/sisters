using UnityEngine;

namespace Code.Classes
{
    public class PlayerMovementController : IMovementController
    {
        public bool LookingRight { get; set; }

        private readonly int layerMask;
        private const float Speed = 4;
        private const float JumpForce = 400;
        private readonly Transform transform;
        private readonly Rigidbody2D rigidBody;
        private readonly Transform groundCheck;

        public PlayerMovementController(GameObject player, Transform groundCheck)
        {
            rigidBody = player.GetComponent<Rigidbody2D>();
            transform = player.GetComponent<Transform>();
            layerMask = 1 << LayerMask.NameToLayer("Ground");
            this.groundCheck = groundCheck;
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
            bool grounded =
                Physics2D.Linecast(transform.position, groundCheck.position, layerMask);
            Debug.Log(grounded);
            if (!grounded)
                return;
            rigidBody.AddForce(Vector2.up * JumpForce);
        }
    }
}
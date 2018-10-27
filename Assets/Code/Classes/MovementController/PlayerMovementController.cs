using UnityEngine;

namespace Code.Classes.MovementController
{
    public class PlayerMovementController : BaseMovementController
    {
        private readonly AudioSource audioSource;
        private readonly Transform groundCheck;
        private readonly AudioClip jump;
        private readonly AudioClip strike;
        public PlayerMovementController(GameObject gameObject, float speed, Transform groundCheck) : base(gameObject)
        {
            this.groundCheck = groundCheck;
            audioSource = gameObject.GetComponent<AudioSource>();
            Speed = speed;
        }

        public override bool CheckGrounded()
        {
            return Physics2D.Linecast(Transform.position, groundCheck.position, LayerMask);
        }

        public override void Jump()
        {
            if (!CheckGrounded())
                return;
            Animator.SetTrigger("Jump");
            PlaySound(jump);
            RigidBody.AddForce(Vector2.up * JumpForce);
        }

        public override void Move(float horizontal)
        {
            ApplyMovement(horizontal);
            Animator.SetInteger("Walking", (int)horizontal);
            TryFlip(horizontal);
        }

        private void ApplyMovement(float horizontal)
        {
            Vector2 currentVelocity = RigidBody.velocity;
            currentVelocity.x = horizontal * Speed;
            RigidBody.velocity = currentVelocity;
        }

        private void Flip()
        {
            Vector3 theScale = Transform.localScale;
            theScale.x *= -1;
            Transform.localScale = theScale;
        }

        private void PlaySound(AudioClip sound)
        {
            audioSource.clip = sound;
            audioSource.Play();
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
    }
}
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Classes.MovementController
{
    public class PlayerMovementController : BaseMovementController
    {
        private readonly AudioSource audioSource;
        private readonly Transform groundCheck;
        private readonly AudioClip jump;

        public PlayerMovementController(GameObject gameObject, float speed, Transform groundCheck) : base(gameObject)
        {
            this.groundCheck = groundCheck;
            audioSource = gameObject.GetComponent<AudioSource>();
            jump = gameObject.GetComponent<Player>().Jump;
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
            Animator.SetInteger("Walking", (int) horizontal);
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
            FlipXAxis(Transform);
            FlipXAxis(Transform.Find("SpeechBubble/BubbleText"));
        }

        public void FlipXAxis(Transform transformToFLip)
        {
            if (transformToFLip == null)
                return;
            Vector3 theScale = transformToFLip.localScale;
            theScale.x *= -1;
            transformToFLip.localScale = theScale;
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
using Code.Classes.MovementController;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class Player : BaseEntity
    {
        public AudioSource Swing;
        public PolygonCollider2D SwordCollider;
        public Transform GroundCheck;

        private Vector3 originalPisition;

        private void Start()
        {
            WalkingSpeed = 4;
            originalPisition = transform.position;
            MovementController = new PlayerMovementController(gameObject, WalkingSpeed, GroundCheck);
        }

        private void Update()
        {
            CheckJump();
            CheckStrike();
        }

        private void FixedUpdate()
        {
            CheckMove();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.tag.Contains("Enemy"))
                return;
            Die();
        }

        private void Die()
        {
            transform.position = originalPisition;
        }

        private void CheckStrike()
        {
            if (!Input.GetButtonDown("Fire1"))
                return;
            Animator.SetTrigger("OnAttackDown");
            Swing.Play();
        }

        private void CheckMove()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            Animator.SetInteger("Walking", (int) horizontal);
            MovementController.Move(horizontal);
        }

        private void CheckJump()
        {
            if (!Input.GetButtonDown("Jump"))
                return;
            MovementController.Jump();
            Animator.SetTrigger("Jump");
        }
    }
}
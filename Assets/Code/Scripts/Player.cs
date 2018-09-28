using System.Collections;
using Code.Classes;
using UnityEngine;

namespace Code.Scripts
{
    public class Player : MonoBehaviour
    {
        public Transform GroundCheck;
        public Animator Animator;
        public AudioSource Swing;
        public PolygonCollider2D SwordCollider;

        private IMovementController movementController;

        private void Start()
        {
            movementController = new PlayerMovementController(gameObject, GroundCheck);
        }

        private void Update()
        {
            CheckJump();
            CheckFire();
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
            Destroy(gameObject);
        }

        private void CheckFire()
        {
            if (!Input.GetButtonDown("Fire1"))
                return;
            Animator.SetTrigger("OnAttackDown");
            Swing.Play();
        }

        private void CheckMove()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            movementController.Move(horizontal);
        }

        private void CheckJump()
        {
            if (Input.GetButtonDown("Jump"))
                movementController.Jump();
        }
    }
}
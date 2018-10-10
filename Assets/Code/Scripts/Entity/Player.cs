using System.Collections.Generic;
using System.Linq;
using Code.Classes.CombatController;
using Code.Classes.MovementController;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public class Player : BaseEntity
    {
        private const int Life = 5;
        public AudioSource Swing;
        public Transform GroundCheck;
        public List<GameObject> Hearts;

        private void Start()
        {
            WalkingSpeed = 4;
            MovementController = new PlayerMovementController(gameObject, WalkingSpeed, GroundCheck);
            CombatController = new PlayerCombatController(gameObject, Life);
        }

        private void Update()
        {
            CheckJump();
            CheckStrike();
            CheckGroundedForJumpAnimation();
        }

        private void CheckGroundedForJumpAnimation()
        {
            Animator.SetBool("Grounded", MovementController.CheckGrounded());
        }

        private void FixedUpdate()
        {
            CheckMove();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.tag.Contains("Enemy"))
                return;
            CombatController.ReceiveHit(collision);
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
            MovementController.Move(horizontal);
        }

        private void CheckJump()
        {
            if (!Input.GetButtonDown("Jump"))
                return;
            MovementController.Jump();
        }
    }
}
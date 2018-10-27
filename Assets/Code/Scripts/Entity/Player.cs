using Code.Classes.CombatController;
using Code.Classes.MovementController;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public abstract class Player : BaseEntity
    {
        public Transform GroundCheck;
        public List<GameObject> Hearts;
        public AudioSource AudioSource;
        public AudioClip Swing;
        public AudioClip Jump;

        protected Dictionary<Control, string> Controls;

        private const int Life = 5;
        private bool movementDisabled;

        public static event Action OnDie;

        protected enum Control
        {
            Horizontal,
            Jump,
            Strike
        }

        public override void Die()
        {
            OnDie?.Invoke();
            base.Die();
        }

        public void DisableMovement()
        {
            MovementController.Move(0);
            movementDisabled = true;
        }

        protected virtual void Start()
        {
            WalkingSpeed = 4;
            MovementController = new PlayerMovementController(gameObject, WalkingSpeed, GroundCheck);
            CombatController = new PlayerCombatController(gameObject, Life);
        }

        private void CheckGroundedForJumpAnimation()
        {
            Animator.SetBool("Grounded", MovementController.CheckGrounded());
        }

        private void CheckJump()
        {
            if (!Input.GetButtonDown(Controls[Control.Jump]))
                return;
            MovementController.Jump();
        }

        private void CheckMove()
        {
            float horizontal = Input.GetAxisRaw(Controls[Control.Horizontal]);
            MovementController.Move(horizontal);
        }

        private void CheckStrike()
        {
            if (!Input.GetButtonDown(Controls[Control.Strike]))
                return;
            Animator.SetTrigger("OnAttackDown");
            AudioSource.clip = Swing;
            AudioSource.Play();
        }

        private void FixedUpdate()
        {
            if (movementDisabled)
                return;
            CheckMove();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.tag.Contains("Enemy"))
                return;
            CombatController.ReceiveHit(collision);
        }

        private void Update()
        {
            if (movementDisabled)
                return;
            CheckJump();
            CheckStrike();
            CheckGroundedForJumpAnimation();
        }
    }
}
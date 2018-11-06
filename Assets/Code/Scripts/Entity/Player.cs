using Code.Classes.CombatController;
using Code.Classes.MovementController;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Entity
{
    public abstract class Player : BaseEntity
    {
        public AudioSource AudioSource;
        public Transform GroundCheck;
        public List<GameObject> Hearts;
        public AudioClip Jump;
        public AudioClip ReceiveHit;
        public AudioClip Swing;
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

        public static void ResetOnDie()
        {
            OnDie = null;
        }

        public override void Die()
        {
            OnDie?.Invoke();
            base.Die();
        }

        public void SetMovement(bool allowed)
        {
            MovementController.Move(0);
            movementDisabled = !allowed;
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
            PlaySound(Swing);
        }

        private void DealWithCollision(GameObject otherGameObject)
        {
            if (!otherGameObject.tag.Contains("Enemy"))
                return;
            PlaySound(ReceiveHit);
            CombatController.ReceiveHit();
        }

        private void FixedUpdate()
        {
            if (movementDisabled)
                return;
            CheckMove();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            DealWithCollision(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            DealWithCollision(col.gameObject);
        }

        private void PlaySound(AudioClip clip)
        {
            AudioSource.clip = clip;
            AudioSource.Play();
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
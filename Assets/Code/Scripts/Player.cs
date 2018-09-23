using Code.Classes;
using UnityEngine;

namespace Code.Scripts
{
    public class Player : MonoBehaviour
    {
        public Transform GroundCheck;
        public Animator Animator;
        public bool AttackPressed;

        private IMovementController movementController;

        private void Start()
        {
            movementController = new PlayerMovementController(gameObject);
        }

        private void Update()
        {
            int layerMask = 1 << LayerMask.NameToLayer("Ground");
            movementController.Grounded =
                Physics2D.Linecast(gameObject.transform.position, GroundCheck.position, layerMask);
        }

        private void FixedUpdate()
        {
            CheckMove();
            CheckJump();
            CheckFire();
        }

        private void CheckFire()
        {
            if (Input.GetButtonDown("Fire1"))
                Animator.SetTrigger("OnAttackDown");
        }

        private void CheckMove()
        {
            float horizontal = Input.GetAxis("Horizontal");
            movementController.Move(horizontal);
        }

        private void CheckJump()
        {
            if (Input.GetButtonDown("Jump"))
                movementController.Jump();
        }
    }
}
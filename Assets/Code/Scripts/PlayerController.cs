using Code.Classes;
using UnityEngine;

namespace Code.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public Transform GroundCheck;

        private IMovementController movementController;

        private void Start()
        {
            movementController = new PlayerMovementController(gameObject);
        }

        private void Update()
        {
            int layerMask = 1 << LayerMask.NameToLayer("Ground");
            movementController.Grounded = Physics2D.Linecast(gameObject.transform.position, GroundCheck.position, layerMask);
        }

        private void FixedUpdate()
        {
            CheckMove();
            CheckJump();
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
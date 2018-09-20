using Code.Classes;
using UnityEngine;

namespace Code.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public LayerMask GroundMask;

        private IMovementController movementController;
        private bool grounded;

        private void Start()
        {
            movementController = new PlayerMovementController(gameObject);
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
            if (Input.GetButtonDown("Jump") && grounded)
                movementController.Jump();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (1 << other.gameObject.layer == GroundMask.value )
                grounded = true;
            Debug.Log(grounded);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (1 << other.gameObject.layer == GroundMask.value)
                grounded = false;
            Debug.Log(grounded);
        }
    }
}
using Code.Classes;
using UnityEngine;

namespace Code.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private IMovementController movementController;
        private void Start()
        {
            movementController = new PlayerMovementController(gameObject);
        }

        private void FixedUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
            movementController.Move(horizontal);
        }
    }
}
using Classes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovementController movementController;
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
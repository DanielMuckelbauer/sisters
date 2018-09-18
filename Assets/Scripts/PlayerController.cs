using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveForce;

    private const float MaxSpeed = 10;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private bool lookingRight = true;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (lookingRight && horizontal < 0)
        {
            lookingRight = false;
            spriteRenderer.flipX = true;
        }
        else if (!lookingRight && horizontal > 0)
        {
            lookingRight = true;
            spriteRenderer.flipX = false;
        }
        if (Mathf.Abs(horizontal) < MaxSpeed)
            rigidBody.AddForce(Vector2.right * horizontal * MoveForce);
    }
}
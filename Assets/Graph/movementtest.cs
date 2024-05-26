using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 5f;
    public LayerMask platformLayerMask;
    public float cornerCorrectionBuffer = 0.2f;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Handle player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Handle jump input
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Handle corner correction during jump
        if (!IsGrounded() && rb.velocity.y > 0)
        {
            CorrectJumpCorner();
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, platformLayerMask);
        return raycastHit.collider != null;
    }

    void CorrectJumpCorner()
    {
        Vector2 playerPos = transform.position;
        Vector2 direction = rb.velocity.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D raycastHit = Physics2D.Raycast(playerPos, direction, cornerCorrectionBuffer, platformLayerMask);

        if (raycastHit.collider != null)
        {
            Bounds platformBounds = raycastHit.collider.bounds;

            if (Mathf.Abs(playerPos.x - platformBounds.min.x) < cornerCorrectionBuffer)
            {
                transform.position = new Vector2(platformBounds.min.x, transform.position.y);
            }
            else if (Mathf.Abs(playerPos.x - platformBounds.max.x) < cornerCorrectionBuffer)
            {
                transform.position = new Vector2(platformBounds.max.x, transform.position.y);
            }
        }
    }
}

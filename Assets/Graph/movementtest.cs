using UnityEngine;

public class LedgeHanging : MonoBehaviour
{
    public Transform ledgeDetector;
    public LayerMask ledgeLayer;
    public float detectionRadius = 0.2f;
    public float hangingOffsetY = 1.0f;

    private bool isHanging = false;
    private Vector2 ledgePosition;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isHanging)
        {
            DetectLedge();
        }

        if (isHanging)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Key to climb up
            {
                ClimbUp();
            }
        }
    }

    void DetectLedge()
    {
        Collider2D ledgeInfo = Physics2D.OverlapCircle(ledgeDetector.position, detectionRadius, ledgeLayer);
        if (ledgeInfo != null)
        {
            ledgePosition = ledgeInfo.transform.position;
            StartHanging();
        }
    }

    void StartHanging()
    {
        isHanging = true;
        rb.velocity = Vector2.zero; // Stop the player's movement
        rb.isKinematic = true; // Disable physics for precise positioning

        // Position the player at the ledge
        transform.position = new Vector2(ledgePosition.x, ledgePosition.y - hangingOffsetY);
    }

    void ClimbUp()
    {
        isHanging = false;
        rb.isKinematic = false; // Re-enable physics
        rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity
        // Add climbing up movement here, if needed
    }

    void OnDrawGizmosSelected()
    {
        if (ledgeDetector != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(ledgeDetector.position, detectionRadius);
        }
    }
}

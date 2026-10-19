using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed;
    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector2(horizontal, vertical).normalized;
        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("Hoe"))
            direction = Vector2.zero;

        AnimateMovement(direction);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    void AnimateMovement(Vector2 direction)
    {
        if (animator == null) return;
        bool isMoving = direction.sqrMagnitude > 0;
        animator.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            animator.SetFloat("horizontal", direction.x);
            animator.SetFloat("vertical", direction.y);
        }
    }
}

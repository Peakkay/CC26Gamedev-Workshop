using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] PlayerAttack attack;
    public float moveSpeed = 5f;
    public float jumpPower = 5f;

    public bool isGrounded;
    public BoxCollider2D feet;
    public LayerMask groundMask;

    public Rigidbody2D rb;
    public SpriteRenderer sprite;


    public bool isFacingRight = true;
    public float lastMoveDir = 1f;


    public void Move(float direction)
    {
        if (!attack.attacking)
        {
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocityY);
        }

        HandleFacing(direction);
    }


    void HandleFacing(float direction)
    {
        if (direction == 0) return;

        lastMoveDir = direction;

        if (direction > 0 && !isFacingRight)
            Flip();

        else if (direction < 0 && isFacingRight)
            Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        sprite.flipX = !sprite.flipX;
    }


    public void Jump()
    {
        groundCheck();

        if (isGrounded && !attack.attacking)
        {
            rb.AddForceY(jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    public void groundCheck()
    {
        isGrounded = feet.IsTouchingLayers(groundMask);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Punch hit Player");
        }
    }

}

using UnityEngine;

public class EnemyMove2 : MonoBehaviour
{
    public float interval = 5f;
    public float counter;
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public SpriteRenderer sprite;

    public bool isFacingRight;
    public bool hasSensed;

    void Start()
    {
        counter = 0f;
        isFacingRight = true;
        hasSensed = false;
    }

    void Update()
    {
        if (!hasSensed)
        {
            counter += Time.deltaTime;
            Count();
        }
    }

    void Count()
    {
        if (counter >= interval)
        {
            counter = 0f;
            moveSpeed = -moveSpeed;
            sprite.flipX = !sprite.flipX;
            isFacingRight = !isFacingRight;
        }
    }

    void FixedUpdate()
    {
        if (!hasSensed)
        {
            Move();
        }
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

    public void ChaseMove(float dir)
    {
        rb.linearVelocity = new Vector2(dir * Mathf.Abs(moveSpeed), rb.linearVelocity.y);

        if (dir > 0 && !isFacingRight) Flip();
        if (dir < 0 && isFacingRight) Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        sprite.flipX = !sprite.flipX;
    }

    public void Stop()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }
}

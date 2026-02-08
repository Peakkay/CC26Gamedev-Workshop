using UnityEngine;
using System.Collections;

public class AryaFight : MonoBehaviour
{
    public EnemyMove2 move;
    public LayerMask playerMask;

    public float senseDistance = 5f;
    public float throwCooldown = 5f;
    float lastThrowTime;
    public Collider2D throwHitbox;
    public bool attacking;
    public Animator animator;
    public float throwDuration = 0.45f;

    public float throwActiveTime = 0.2f;
    public float throwDelay = 0.5f;
    Transform player;

    void Start()
    {
        throwHitbox.enabled = false;
    }
    void Update()
    {
        TrySense();

        if (move.hasSensed && player != null && !attacking)
        {
            HandleCombat();
        }
    }

    void TrySense()
    {
        Vector2 origin = transform.position;
        Vector2 dir = move.isFacingRight ? Vector2.right : Vector2.left;

        Collider2D hit = Physics2D.OverlapCircle(origin, senseDistance, playerMask);

        //Debug.DrawRay(origin, dir * throwRange, Color.red);

        if (hit != null)
        {
            move.hasSensed = true;
            player = hit.transform;
        }
        else
        {
            move.hasSensed = false;
            player = null;
        }
    }

    void HandleCombat()
    {
    }

    void TryThrow()
    {
        if (Time.time - lastThrowTime < throwCooldown)
            return;

        lastThrowTime = Time.time;
        Debug.Log("Enemy Throw!");
        StartCoroutine(Throw());
    }

    public void EnableThrow()
    {
        if(move.isFacingRight)
        {
            throwHitbox.offset = new Vector2(1.0657f,0.5936f);
        } else
        {
            throwHitbox.offset = new Vector2(-1.0657f,0.5936f);
        }
        throwHitbox.enabled = true;
    }

    public void DisableThrow()
    {
        throwHitbox.enabled = false;
    }

    public IEnumerator Throw()
    {
        attacking = true;
        yield return new WaitForSeconds(throwDelay);
        animator.SetTrigger("Throw");

        yield return new WaitForSeconds(0.15f);  // windup

        EnableThrow();
        yield return new WaitForSeconds(throwActiveTime);
        DisableThrow();

        yield return new WaitForSeconds(throwDuration - 0.15f - throwActiveTime);
        attacking = false;
    }

}

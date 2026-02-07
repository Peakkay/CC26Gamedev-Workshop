using UnityEngine;
using System.Collections;

public class EnemyFight : MonoBehaviour
{
    public EnemyMove move;
    public LayerMask playerMask;

    public float senseDistance = 5f;
    public float punchRange = 1f;
    public float punchCooldown = 1.2f;
    float lastPunchTime;
    public Collider2D punchHitbox;
    public bool attacking;
    public Animator animator;
    public float punchDuration = 0.45f;

    public float punchActiveTime = 0.2f;
    public float punchDelay = 0.5f;
    Transform player;

    void Start()
    {
        punchHitbox.enabled = false;
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

        RaycastHit2D hit = Physics2D.Raycast(origin, dir, senseDistance, playerMask);

        Debug.DrawRay(origin, dir * punchRange, Color.red);

        if (hit.collider != null)
        {
            move.hasSensed = true;
            player = hit.collider.transform;
        }
        else
        {
            move.hasSensed = false;
            player = null;
        }
    }

    void HandleCombat()
    {
        float dist = Mathf.Abs(player.position.x - transform.position.x);
        Debug.Log("Distance: "+dist);
        if (dist <= punchRange)
        {
            Debug.Log("Range");
            move.Stop();
            TryPunch();
            return;
        }

        float dir = Mathf.Sign(player.position.x - transform.position.x);
        Debug.Log("Direction: "+dir);
        move.ChaseMove(dir);
    }

    void TryPunch()
    {
        if (Time.time - lastPunchTime < punchCooldown)
            return;

        lastPunchTime = Time.time;
        Debug.Log("Enemy Punch!");
        StartCoroutine(Punch());
    }

    public void EnablePunch()
    {
        if(move.isFacingRight)
        {
            punchHitbox.offset = new Vector2(1.0657f,0.5936f);
        } else
        {
            punchHitbox.offset = new Vector2(-1.0657f,0.5936f);
        }
        punchHitbox.enabled = true;
    }

    public void DisablePunch()
    {
        punchHitbox.enabled = false;
    }

    public IEnumerator Punch()
    {
        attacking = true;
        yield return new WaitForSeconds(punchDelay);
        animator.SetTrigger("Punch");

        yield return new WaitForSeconds(0.15f);  // windup

        EnablePunch();
        yield return new WaitForSeconds(punchActiveTime);
        DisablePunch();

        yield return new WaitForSeconds(punchDuration - 0.15f - punchActiveTime);
        attacking = false;
    }

}

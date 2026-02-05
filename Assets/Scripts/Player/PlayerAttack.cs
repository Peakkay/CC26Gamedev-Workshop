using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public PlayerMove player;
    public Animator animator;
    public Collider2D kickHitbox;
    public float kickCooldown = 1.2f;
    public float lastKickTime = 0f;
    public float kickDuration = 0.45f;

    public float kickActiveTime = 0.2f;
    public bool attacking = false;

    void Start()
    {
        kickHitbox.enabled = false;
    }

    public void TryKick()
    {
        if (Time.time - lastKickTime < kickCooldown || attacking)
            return;

        lastKickTime = Time.time;
        Debug.Log("Player Kick!");
        StartCoroutine(Kick());
    }

    public IEnumerator Kick()
    {
        attacking = true;
        animator.SetTrigger("Kick");

        yield return new WaitForSeconds(0.15f);  // windup

        EnableKick();
        yield return new WaitForSeconds(kickActiveTime);
        DisableKick();

        yield return new WaitForSeconds(kickDuration - 0.15f - kickActiveTime);
        attacking = false;
        
    }

    public void EnableKick()
    {
        if(player.isFacingRight)
        {
            kickHitbox.offset = new Vector2(0.8125572f,0.9870659f);
        } else
        {
            kickHitbox.offset = new Vector2(-0.8125572f,0.9870659f);
        }
        kickHitbox.enabled = true;
    }

    public void DisableKick()
    {
        kickHitbox.enabled = false;
    }

}

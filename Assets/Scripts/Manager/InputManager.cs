using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] PlayerMove player;
    [SerializeField] PlayerAttack attack;
    float horizontal = 0f;
    bool jumpcall = false;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        //Debug.Log("Horizontal: "+horizontal);
        if(Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump Called");
            jumpcall = true;
        }
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Attack Called");
            if(!attack.attacking)
            {
                attack.TryKick();
            }
        }
    }

    void FixedUpdate()
    {
        player.Move(horizontal);
        if(jumpcall)
        {
            player.Jump();
            jumpcall = false;
        }
    }
}

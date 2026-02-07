using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SceneManagement;

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(player == null){player = FindFirstObjectByType<PlayerMove>();}
        if(attack == null){attack = FindFirstObjectByType<PlayerAttack>();}
        
        Debug.Log("Rebound player references");
    }

}

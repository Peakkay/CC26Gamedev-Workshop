using UnityEngine;

public class End2 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Reached the End game complete");
        }
        Application.Quit();    
    }
}

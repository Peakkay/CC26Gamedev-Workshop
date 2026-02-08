using UnityEngine;
using UnityEngine.SceneManagement;   

public class EnemyPunch2 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Punch hit Player!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

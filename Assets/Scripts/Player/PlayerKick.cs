using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Kick hit on Enemy");
            GameObject.Destroy(other.gameObject);
        }
    }
}

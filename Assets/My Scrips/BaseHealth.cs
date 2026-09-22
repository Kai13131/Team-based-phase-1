using Unity.VisualScripting;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            GameManager.Instance.baseHealth -= 1;
        }
    }
}

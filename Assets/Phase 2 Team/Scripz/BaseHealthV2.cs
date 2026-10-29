using Unity.VisualScripting;
using UnityEngine;

public class BaseHealthV2 : MonoBehaviour
{
    [Header("Base Health")]

    public int maxHealth = 100;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {

        // reduce health when enemy touches wall

        currentHealth -= damage;

        Debug.Log("Base HP: " + currentHealth);

        if (currentHealth < 0)
        {
            DestroyBase();
        }

    }
    
    private void DestroyBase()
    {
        // Destroy base when enemys reduce hp to 0.

        Destroy(gameObject);
    }
}


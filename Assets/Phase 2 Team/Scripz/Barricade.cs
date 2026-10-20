using UnityEngine;

public class Barricade : MonoBehaviour
{
    [Header("Barricade Health")]

    public int maxHealth = 10;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {

        // reduce health when enemy touches wall

        currentHealth -= damage;

        Debug.Log("Barricade HP: " + currentHealth);

        if (currentHealth < 0)
        {
            DestroyBarricade();
        }

    }

    private void DestroyBarricade()
    {
        // Destroy barricade when enemys reduce hp to 0.

        Destroy(gameObject);
    }
}

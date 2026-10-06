using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 3; // Maximum health of the enemy

    public void SetHealth()
    {
        int wave = GameManager.Instance.currentWave;
        health = health * wave * 0.5f;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took damage. Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died.");
        GameManager.Instance.AddMoney(10); // Add money to the player's total when the enemy dies
        Destroy(gameObject); // Destroy the enemy game object
    }

}

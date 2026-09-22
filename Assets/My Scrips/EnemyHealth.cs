using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3; // Maximum health of the enemy

    public void SetHealth(int mount)
    {
        health = mount; // Set the enemy's health to the specified amount
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
        GameManager.Instance.ScoreUpdate(5);
        Destroy(gameObject); // Destroy the enemy game object
    }

}

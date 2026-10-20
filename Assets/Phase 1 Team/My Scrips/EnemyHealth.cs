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
        Debug.Log("Die() called. Tag is: " + gameObject.tag);

        if (gameObject.CompareTag("Boss"))
        {
            Debug.Log("BOSS DIED!");

            GameManager.Instance.WinGame();

            return;
        }

        Debug.Log("Enemy died.");

        GameManager.Instance.AddMoney(10);

        Destroy(gameObject);

    }

}

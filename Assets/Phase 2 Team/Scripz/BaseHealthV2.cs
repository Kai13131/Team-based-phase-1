using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BaseHealthV2 : MonoBehaviour
{
    [Header("Base Health")]

    public int maxHealth = 100;

    private float currentHealth;

    [SerializeField] private TMP_Text baseHealthText;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {

        // reduce health when enemy touches wall

        currentHealth = Mathf.Max(0, currentHealth - damage);

        UpdateHealthUI();

        Debug.Log("Base HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            DestroyBase();
        }

    }
    
    private void DestroyBase()
    {
        // Destroy base when enemys reduce hp to 0.

        GameManager.Instance.baseHealth = 0;
        GameManager.Instance.BaseDestroyed();

        Destroy(gameObject);
    }

    private void UpdateHealthUI()
    {
        // Display current and maximum health.
        if (baseHealthText != null)
        {
            baseHealthText.text = "Base HP: " +
                currentHealth + " / " + maxHealth;
        }
    }
}


using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 100;
    public int baseHealth = 10;
    public int currentWave = 0;
    public float score;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        BaseDestroyed(); // Check if the base has been destroyed and handle game over logic if necessary
    }

    public void AddMoney(int mount)
    {
        money += mount;
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true; // Purchase successful
        }
        else
        {
            return false; // Not enough money
        }
    }

    public void ScoreUpdate(int mount)
    {
        score += mount;
    }

    public void GetCurrentWave(int wave)
    {
        currentWave = wave;
    }

    public void BaseDestroyed()
    {
        if (baseHealth <= 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("GameOverScene");
            // Implement game over logic here (e.g., show game over screen, restart game, etc.)

        }
    }

}

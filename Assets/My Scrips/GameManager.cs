using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 100;
    public int baseHealth = 10;
    public int currentWave = 0;
    public float score;

    public GameObject gameOverPanel;
    public GameObject gamePausePanel;


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
        Pause();
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

    public void GetCurrentWave(int wave)
    {
        currentWave = wave;
    }

    public void BaseDestroyed()
    {
        if (baseHealth <= 0)
        {
            Debug.Log("Game Over!");
            gameOverPanel.SetActive(true);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gamePausePanel.activeSelf)
        {
            Time.timeScale = 0f;
            gamePausePanel.SetActive(true);
            Debug.Log("pause");
        }
        if (Input.GetKeyDown(KeyCode.Escape) && gamePausePanel.activeSelf)
        {
            Time.timeScale = 1f;
            gamePausePanel.SetActive(false);
            Debug.Log("Continues");
        }
    }
}

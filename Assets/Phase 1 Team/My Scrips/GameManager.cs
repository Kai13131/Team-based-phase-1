using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //public int money = 100;
    public int baseHealth = 10;
    public int currentWave = 0;
    public float score;

    [Header("Tower Efficiency")]
    public int maxEfficiency = 10;

    public int currentEfficiency = 0;

    public GameObject gameOverPanel;
    public GameObject gamePausePanel;
    public GameObject gameWinPanel;

    [Header("Fast Forward Time Scale")]
    [SerializeField] private float fastForward = 4f;

    private bool isPaused = false;
    private bool isFastForwarding = false;
    private float defaultFixedDeltaTime;

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

        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        BaseDestroyed();
        Pause();
        FastForward();
        ApplyTimeScale();
    }

    private void ApplyTimeScale()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else if (BuildManager.Instance != null &&
                 BuildManager.Instance.IsCurrentlyBuilding)
        {
            Time.timeScale = 0.5f;
        }
        else if (isFastForwarding)
        {
            Time.timeScale = fastForward;
        }
        else
        {
            Time.timeScale = 1f;
        }

        Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
    }

    public bool SpendEfficiency(int amount)
    {
        // Check if the tower would exceed capacity.
        if (amount < 0 || currentEfficiency + amount > maxEfficiency)
            return false;

        currentEfficiency += amount;
        return true;
    }

    public void RefundEfficiency(int amount)
    {
        // Return efficiency when a tower is removed.
        currentEfficiency = Mathf.Max(0, currentEfficiency - Mathf.Max(0, amount));

    }

    //public void AddMoney(int mount)
    //{
    //    money += mount;
    //}

    //public bool SpendMoney(int amount)
    //{
    //    if (money >= amount)
    //    {
    //        money -= amount;
    //        return true; // Purchase successful
    //    }
    //    else
    //    {
    //        return false; // Not enough money
    //    }
    //}

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
        Debug.Log("Restart button clicked!");

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            gamePausePanel.SetActive(true);
            Debug.Log("Pause");
        }
        else
        {
            Time.timeScale = isFastForwarding ? fastForward : 1f;
            gamePausePanel.SetActive(false);
            Debug.Log("Continues");
        }
    }

    public void FastForward()
    {
        if (!Input.GetKeyDown(KeyCode.F) || isPaused)
            return;

        isFastForwarding = !isFastForwarding;
        Time.timeScale = isFastForwarding ? fastForward : 1f;
        Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;

        Debug.Log(isFastForwarding ? "Fast Forward" : "Normal Speed");
    }

    public void WinGame()
    {
        //isGameEnded = true;
        isFastForwarding = false;
        isPaused = false;

        gameWinPanel.SetActive(true);

        Time.timeScale = 0f;
        Time.fixedDeltaTime = defaultFixedDeltaTime;

        Debug.Log("YOU WIN!");
    }
}

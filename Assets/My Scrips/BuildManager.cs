using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    public GameObject HolyTowerPrefab;
    public GameObject IceTowerPrefab;
    public GameObject MagicTowerPrefab;

    private GameObject selectedTower;

    public GameObject buildTiles;

    public AudioSource audioSource;
    public AudioClip selectSound;
    public AudioClip buildedSound;

    void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedTower = HolyTowerPrefab;
        buildTiles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedTower = null;
            buildTiles.SetActive(false);
            audioSource.PlayOneShot(selectSound);

            Debug.Log("Deselected Tower");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedTower = HolyTowerPrefab;
            Debug.Log("Selected Standard Tower");
            audioSource.PlayOneShot(selectSound);

            buildTiles.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTower = IceTowerPrefab;
            Debug.Log("Selected Slow Tower");
            audioSource.PlayOneShot(selectSound);

            buildTiles.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedTower = MagicTowerPrefab;
            Debug.Log("Selected Advanced Tower");
            audioSource.PlayOneShot(selectSound);

            buildTiles.SetActive(true);
        }
    }

    public bool BuildTower(Vector3 position, BuildTile tile)
    {
        if (selectedTower == null)
        {
            Debug.Log("Please select a tower first!");
            return false;
        }

        Towers towerData = selectedTower.GetComponent<Towers>();

        if (towerData == null)
        {
            Debug.LogError("Selected tower does not have a Towers component!");
            return false;
        }

        if (GameManager.Instance.SpendMoney(towerData.cost))
        {
            GameObject newTower = Instantiate(selectedTower, position, Quaternion.identity);
            tile.SetTower(newTower);

            buildTiles.SetActive(false);

            audioSource.PlayOneShot(buildedSound);

            return true;
        }
        else
        {
            Debug.Log("Not enough money!");
            return false;
        }
    }
}

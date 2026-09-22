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
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public bool BuildTower(Vector3 position)
    {
        Towers towerData = selectedTower.GetComponent<Towers>();

        if (GameManager.Instance.SpendMoney(towerData.cost))
        {
            Instantiate(selectedTower, position, Quaternion.identity);
            buildTiles.SetActive(false);

            audioSource.PlayOneShot(buildedSound);

            return true; // Tower built successfully
        }
        else
        {
            Debug.Log("Not enough money!");
        }
        return false;
    }
}

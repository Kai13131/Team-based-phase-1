using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    //Tower prefabs
    public GameObject HolyTowerPrefab;
    public GameObject IceTowerPrefab;
    public GameObject MagicTowerPrefab;

    private GameObject selectedTower;

    public GameObject buildTiles;
    public float h_offset = 0.5f;


    [Header("Building Time Scale")]
    [SerializeField] private float buildTimeScale = 0.5f;
    private bool isCurrentlyBuilding = false;


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
            isCurrentlyBuilding = !isCurrentlyBuilding;
            selectedTower = null;
            buildTiles.SetActive(false);
            Debug.Log("Deselected Tower");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isCurrentlyBuilding = true;
            selectedTower = HolyTowerPrefab;
            Debug.Log("Selected Standard Tower");
            buildTiles.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isCurrentlyBuilding = true;
            selectedTower = IceTowerPrefab;
            Debug.Log("Selected Slow Tower");
            buildTiles.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isCurrentlyBuilding = true;
            selectedTower = MagicTowerPrefab;
            Debug.Log("Selected Advanced Tower");
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

        // Check if the tower exceeds available efficiency.
        if (GameManager.Instance.currentEfficiency + towerData.efficiencyCost >
            GameManager.Instance.maxEfficiency)
        {
            Debug.Log("Not enough efficiency!");
            return false;
        }

        // Spawn the selected tower.

        //Height offset add
        position = new Vector2(position.x, position.y + h_offset);

        GameObject newTower = Instantiate(selectedTower,position,Quaternion.identity);

        // Get the existing Towers component.
        Towers newTowerScript = newTower.GetComponent<Towers>();

        // Register efficiency before placing the tower.
        if (newTowerScript == null || !newTowerScript.RegisterEfficiency())
        {
            Destroy(newTower);
            return false;
        }

        // Assign tower to its build tile.
        tile.SetTower(newTower);
        newTowerScript.SetBuildTile(tile);

        buildTiles.SetActive(false);

        Debug.Log("Tower built!");
        isCurrentlyBuilding = false;
        return true;
    }

    public bool IsCurrentlyBuilding => isCurrentlyBuilding;
}
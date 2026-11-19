
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private bool isCurrentlyBuilding;
    [SerializeField] private GameObject selectedTower;
    [SerializeField] public GameObject buildTiles;

    [SerializeField] public GameObject HolyTowerPrefab;
    [SerializeField] public GameObject IceTowerPrefab;
    [SerializeField] public GameObject MagicTowerPrefab;



    public Button Extrasettingsbutton;
    public Button Extrasettingsbuttonclose;
    public GameObject Extrasettingstab;
    public Button Extrasettingspause;
    public Button Extrasettingsfastfoward;
    public Button Towerbuildbutton;
    public Button Towerbuildbuttonclose;
    public GameObject Towerbuildtab;
    public Button Towerbuildbutton1;
    public Button Towerbuildbutton2;
    public Button Towerbuildbutton3; 




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button Extrasettingsbutton = gameObject.GetComponent<Button>();
        Extrasettingsbutton.onClick.AddListener(OpenSettings);

        Button Extrasettingsbuttonclose = gameObject.GetComponent<Button>();
        Extrasettingsbutton.onClick.AddListener(CloseSettings);

        Button Towerbuildbutton = gameObject.GetComponent<Button>();
        Towerbuildbutton.onClick.AddListener(OpenTowerTab);

        Button Towerbuildbuttonclose = gameObject.GetComponent<Button>();
        Towerbuildbutton.onClick.AddListener(CloseTowerTab);

        Button Towerbuildbutton1 = gameObject.GetComponent<Button>();
        Towerbuildbutton.onClick.AddListener(PlaceTower1);

        Button Towerbuildbutton2 = gameObject.GetComponent<Button>();
        Towerbuildbutton.onClick.AddListener(PlaceTower2);

        Button Towerbuildbutton3 = gameObject.GetComponent<Button>();
        Towerbuildbutton.onClick.AddListener(PlaceTower3);

        Extrasettingstab.SetActive(false);
        Towerbuildtab.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {



    }

    public void OpenSettings()
    {

        Extrasettingstab.SetActive(true);
        Debug.Log("Settings Open");

    }

    public void CloseSettings()
    {

        Extrasettingstab.SetActive(false);
        Debug.Log("Settings Closed");

    }

    public void OpenTowerTab()
    {

        Towerbuildtab.SetActive(true);
        Debug.Log("Tower Tab Open");


    }

    public void CloseTowerTab()
    {

        Towerbuildtab.SetActive(false);
        Debug.Log("Tower Tab Close");


    }

    public void PlaceTower1()
    {

        Towerbuildtab.SetActive(false);
        Debug.Log("Tower 1 Placement");

        isCurrentlyBuilding = true;
        selectedTower = HolyTowerPrefab;
        Debug.Log("Selected Standard Tower");
        buildTiles.SetActive(true);


    }

    public void PlaceTower2()
    {

        Towerbuildtab.SetActive(false);
        Debug.Log("Tower 2 Placement");


    }

    public void PlaceTower3()
    {

        Towerbuildtab.SetActive(false);
        Debug.Log("Tower 3 Placement");


    }

}

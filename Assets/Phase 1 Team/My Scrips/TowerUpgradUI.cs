using TMPro;
using UnityEngine;

public class TowerUpgradeUI : MonoBehaviour
{
    public static TowerUpgradeUI Instance;

    public GameObject upgradePanel;

    public TMP_Text levelText;
    public TMP_Text damageText;
    public TMP_Text attackSpeedText;
    public TMP_Text rangeText;
    public TMP_Text upgradeCostText;

    private Towers selectedTower;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradePanel.SetActive(false);
    }
    public void SelectTower(Towers tower)
    {
        selectedTower = tower;

        upgradePanel.SetActive(true);

        UpdateUI();
    }
    public void UpgradeTower()
    {
        if (selectedTower == null)
        {
            Debug.Log("No tower selected!");

            return;
        }

        selectedTower.Upgrade();

        UpdateUI();
    }

    public void DeleteTower()
    {
        if (selectedTower == null)
        {
            Debug.LogWarning("There is no tower assigned to this tile!");
            return;
        }

        // Get the BuildTile that owns this tower
        BuildTile tile = selectedTower.GetBuildTile();

        if (tile == null)
        {
            Debug.LogError("This tower is not connected to a BuildTile!");
            return;
        }

        int refundAmount = selectedTower.cost;

        // Give money back
        GameManager.Instance.AddMoney(refundAmount);

        // Remove tower from the tile
        tile.RemoveTower();

        // Destroy the tower
        Destroy(selectedTower.gameObject);
        Debug.Log("Tower deleted! Refunded $" + refundAmount);
        selectedTower = null;

        // Close UI
        upgradePanel.SetActive(false);
    }

    public void ClosePanel()
    {
        selectedTower = null;

        upgradePanel.SetActive(false);
    }

    private void UpdateUI()
    {
        if (selectedTower == null)
        {
            return;
        }

        levelText.text =
            "Level: " + selectedTower.upgradeLevel;

        damageText.text =
            "Damage: " + selectedTower.bulletDamage;

        attackSpeedText.text =
            "Attack Speed: " +
            selectedTower.attackSpeed.ToString("F1");

        rangeText.text =
            "Range: " +
            selectedTower.range.ToString("F1");

        if (selectedTower.upgradeLevel >=
            selectedTower.maxUpgradeLevel)
        {
            upgradeCostText.text = "MAX LEVEL";
        }
        else
        {
            upgradeCostText.text =
                "Upgrade: $" +
                selectedTower.upgradeCost;
        }
    }
}

using UnityEngine;

public class BuildTile : MonoBehaviour
{
    public bool occupied = false;
    private GameObject currentTower;
    private BuildTile buildTile;

    private void OnMouseDown()
    {
        Debug.Log("Clicked Build Tile");

        if (occupied)
        {
            Debug.Log("Tile is already occupied");
            return;
        }

        if (BuildManager.Instance == null)
        {
            Debug.LogError("BuildManager.Instance is missing!");
            return;
        }

        bool built = BuildManager.Instance.BuildTower(transform.position,this);

        if (built)
        {
            occupied = true;
            Debug.Log("Tower built!");  
        }
    }
    public void SetTower(GameObject tower)
    {
        currentTower = tower;
        occupied = true;

        Debug.Log("Tower assigned to tile!");
    }
    public void DeleteTower()
    {
        if (currentTower == null)
        {
            Debug.LogWarning("There is no tower assigned to this tile!");
            return;
        }

        Destroy(currentTower);
        currentTower = null;

        occupied = false;

        GameManager.Instance.AddMoney(50);

        Debug.Log("Tower deleted! Refunded 50 money.");
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right-click detected on Build Tile");

            if (occupied)
            {
                DeleteTower();
            }
            else
            {
                Debug.Log("There is no tower on this tile.");
            }
        }
    }
}

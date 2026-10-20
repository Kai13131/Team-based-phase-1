using UnityEngine;

public class BuildTile : MonoBehaviour
{
    public bool occupied = false;
    private GameObject currentTower;

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

    public GameObject GetTower()
    {
        return currentTower;
    }
    public void RemoveTower()
    {
        currentTower = null;
        occupied = false;

        Debug.Log("Build tile is now empty!");
    }
}

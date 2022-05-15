using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool canBuild;
    public GameObject building;
    public Building buildingInfo;
    private bool occupied = false;

    private Color baseColor;
    private Renderer ren;

    public void Start()
    {
        ren = GetComponent<Renderer>();
        baseColor = ren.material.color;
    }

    public void OnMouseEnter()
    {
        //canBuild = BuildingManager.Instance.CanBudid;
        if (canBuild)
        {
            ren.material.color = Color.green;
        }
        else
        {
            ren.material.color = Color.red;
        }
    }

    public void OnMouseExit()
    {
        ren.material.color = baseColor;
    }

    public void OnMouseDown()
    {
        if (BuildingManager.Instance.sellMode == true && building != null)
        {
            SellBuilding();
            Debug.Log("Sold tile " + this);
            occupied = false;
            return;
        }
        if(BuildingManager.Instance.sellMode == true && !occupied)
        {
            UIManager.Instance.ShowMessage("Tile has no building to sell.");
            return;
        }
        if (canBuild && !occupied)
        {
            BuildingManager.Instance.Build(this);
            occupied = true;
            return;
        }
        else if (occupied)
        {
            UIManager.Instance.ShowMessage("Tile is occupied by another building.");
        }
        else
        {
            UIManager.Instance.ShowMessage("Cant Build on this tile.");
        }
    }
    public void InstantiateBuilding(Building _building)
    {
        if (building != null)
        {
            return;
        }
        else
        {
            building = (GameObject)Instantiate(_building.model, new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.identity);
            BuildingManager.Instance.AddToList(building);
        }
    }

    public void SellBuilding()
    {
        PlayerStats.Instance.currentMoney += buildingInfo.sellValue;
        buildingInfo = null;
        Destroy(building);
        building = null;
        UIManager.Instance.UpdateUI();
    }


}

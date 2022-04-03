using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool canBuild;
    public GameObject building;

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
        if (canBuild)
        {
            BuildingManager.Instance.Build(this);
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
}

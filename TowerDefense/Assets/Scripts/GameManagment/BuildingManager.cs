using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public BuildingsLibrary availableBuildings;

    private Building selectedBuilding;

    public List<GameObject> placedBuildings;

    public bool CanBuild { get { return selectedBuilding != null ? PlayerStats.Instance.HasEnoughMoney(selectedBuilding.price) : false; } }
    public static BuildingManager Instance { get; private set; }

    public bool sellMode = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
    public void Build(Tile tile)
    {
        if (sellMode)
        {
            DestroyClosestBuilding(tile);
            return;
        }
        if (selectedBuilding == null)
        {
            UIManager.Instance.ShowMessage("No Building Selected!");
            return;
        }
        if (PlayerStats.Instance.HasEnoughMoney(selectedBuilding.price))
        {
            PlayerStats.Instance.currentMoney -= selectedBuilding.price;
            UIManager.Instance.UpdateUI();
            tile.InstantiateBuilding(selectedBuilding);
        }
        else
        {
            UIManager.Instance.ShowMessage("Not Enough Money!");
        }
    }

    public void AddToList(GameObject obj)
    {
        placedBuildings.Add(obj);
    }

    public void RemoveFromList(GameObject obj)
    {
        placedBuildings.Remove(obj);
    }

    public void DestroyClosestBuilding(Tile tile)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach(GameObject obj in placedBuildings)
        {
            if(Vector3.Distance(tile.gameObject.transform.position, obj.transform.position) < distance)
            {
                closest = obj;
                distance = Vector3.Distance(tile.gameObject.transform.position, obj.transform.position);
            }
        }
        if(closest != null)
        {
            RemoveFromList(closest);
            Destroy(closest);
        }
    }

    public void ChangeSellMode()
    {
        if (sellMode == true)
        {
            sellMode = false;
            UIManager.Instance.ShowMessage("Sell Mode Disabled.");
        }
        else
        {
            sellMode = true;
            UIManager.Instance.ShowMessage("Sell Mode Enabled.");
        }
    }


    public void SelectBuilding(int id)
    {
        if (id >= availableBuildings.buildings.Length)
        {
            UIManager.Instance.ShowMessage("No Building Selected!");
            selectedBuilding = null;
            return;
        }
        selectedBuilding = availableBuildings.buildings[id];
        UIManager.Instance.ShowMessage("Selected " + selectedBuilding.name);
    }
}

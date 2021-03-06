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
        if (selectedBuilding == null)
        {
            UIManager.Instance.ShowMessage("No Building Selected!");
            return;
        }
        if (PlayerStats.Instance.HasEnoughMoney(selectedBuilding.price))
        {
            PlayerStats.Instance.currentMoney -= selectedBuilding.price;
            UIManager.Instance.UpdateUI();
            tile.buildingInfo = selectedBuilding;
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


    public void SelectBuilding(Building selection)
    {        
        selectedBuilding = selection;
        UIManager.Instance.ShowMessage("Selected " + selectedBuilding.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public BuildingsLibrary availableBuildings;

    private Building selectedBuilding;
    private int selectedBuildingIndex;

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
            NetworkManager.instance.BuildSignal(selectedBuildingIndex, tile.transform.position.x, tile.transform.position.z);
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
    public void BuildNT(int bID, float xPos, float yPos)
    {
        Building building = availableBuildings.buildings[bID];
        Instantiate(building.model, new Vector3(xPos, 0f, yPos), Quaternion.identity);
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
        selectedBuildingIndex = availableBuildings.GetBuildingID(selection);
        UIManager.Instance.ShowMessage("Selected " + selectedBuilding.name);
    }
}

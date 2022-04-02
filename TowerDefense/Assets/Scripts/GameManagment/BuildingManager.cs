using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public BuildingsLibrary availableBuildings;

    private Building selectedBuilding;

    public bool CanBudid { get { return selectedBuilding != null ? PlayerStats.Instance.HasEnoughMoney(selectedBuilding.price) : false; } }
    public static BuildingManager Instance { get; private set; }

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
            return;
        }
        tile.InstantiateBuilding(selectedBuilding);
    }
    public void SelectBuilding(int id)
    {
        if (id >= availableBuildings.buildings.Length)
        {
            Debug.LogError("Wrong building ID. Check selection");
            selectedBuilding = null;
            return;
        }
        selectedBuilding = availableBuildings.buildings[id];
    }
}

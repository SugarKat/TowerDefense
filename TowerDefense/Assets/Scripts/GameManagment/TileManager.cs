using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<Building> availableBuildings;

    public void Build(Tile tile, Building building)
    {
        if(tile.buildable && availableBuildings.Contains(building))
        {
            Instantiate(building.model, tile.model.transform, false);
        }
    }
}

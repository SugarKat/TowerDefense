using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New B Library", menuName = "Libraries/BuildingsLibrary")]
public class BuildingsLibrary : ScriptableObject
{
    public Building[] buildings;

    public int GetBuildingID(Building building)
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            if (building.Equals(buildings[i]))
            {
                return i;
            }
        }
        return -1;
    }
}

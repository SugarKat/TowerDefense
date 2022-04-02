using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New B Library", menuName = "Libraries/BuildingsLibrary")]
public class BuildingsLibrary : ScriptableObject
{
    public Building[] buildings;
}

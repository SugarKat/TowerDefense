using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Libraries/Building")]
public class Building : ScriptableObject
{
    public new string name;
    public int price;
    public GameObject model;
}

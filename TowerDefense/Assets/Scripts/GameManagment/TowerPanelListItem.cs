using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerPanelListItem : MonoBehaviour
{
    public new TextMeshProUGUI name;
    public TextMeshProUGUI price;

    Building selection;

    public void Setup(Building blueprint)
    {
        selection = blueprint;
        name.text = selection.name;
        price.text = selection.price.ToString();
    }
    public void SelectBuilding()
    {
        BuildingManager.Instance.SelectBuilding(selection);
    }
}

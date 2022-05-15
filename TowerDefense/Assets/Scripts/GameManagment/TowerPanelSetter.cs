using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPanelSetter : MonoBehaviour
{
    public GameObject buildingListItem;
    public Transform towerListParent;

    List<GameObject> towerList = new List<GameObject>();

    private void Start()
    {
        UpdateTowerList();
    }

    void UpdateTowerList()
    {
        ClearBuildList();

        foreach (var item in BuildingManager.Instance.availableBuildings.buildings)
        {
            GameObject _buildListItemGO = Instantiate(buildingListItem);
            _buildListItemGO.transform.SetParent(towerListParent);

            TowerPanelListItem _buildListItem = _buildListItemGO.GetComponent<TowerPanelListItem>();
            _buildListItem.Setup(item);

            towerList.Add(_buildListItemGO);
        }
    }
    void ClearBuildList()
    {
        for (int i = 0; i < towerList.Count; i++)
        {
            Destroy(towerList[i]);
        }
        towerList.Clear();
    }
}

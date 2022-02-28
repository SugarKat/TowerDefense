using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI livesTxt;
    public TextMeshProUGUI moneyTxt;
    public Animation towerPanel;
    public GameObject closeButton;
    public GameObject openButton;

    public void UpdateLives(int ammount)
    {
        livesTxt.text = $"Lives: {ammount}";
    }
    public void UpdateMoney(int ammount)
    {
        moneyTxt.text = $"Money: {ammount}";
    }
    public void OpenTowerPanel()
    {
        openButton.SetActive(false);
        closeButton.SetActive(true);
        towerPanel.Play("TowerPanelOpening");
    }
    public void CloseTowerPanel()
    {
        openButton.SetActive(true);
        closeButton.SetActive(false);
        towerPanel.Play("TowerPanelClosing");
    }
}

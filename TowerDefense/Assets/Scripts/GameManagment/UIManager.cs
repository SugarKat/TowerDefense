using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI livesTxt;
    public TextMeshProUGUI moneyTxt;
    public TextMeshProUGUI roundTxt;
    public TextMeshProUGUI enemyTxt;
    public TextMeshProUGUI errorTxt;
    public Animation towerPanel;
    public GameObject closeButton;
    public GameObject openButton;
    private string errorText = "";

    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateLives()
    {
        livesTxt.text = $"Lives: {PlayerStats.Instance.currentHealth}";
    }
    public void UpdateMoney()
    {
        moneyTxt.text = $"Money: {PlayerStats.Instance.currentMoney}";
    }

    public void UpdateEnemiesLeft()
    {
        enemyTxt.text = $"Enemies Left: {RoundManager.Instance.enemiesLeft}";
    }

    public void UpdateRoundNR()
    {
        if(RoundManager.Instance.currentRoundNR < 0)
        {
            roundTxt.text = $"Game Not Started";
        }
        roundTxt.text = $"Round: {RoundManager.Instance.currentRoundNR + 1}";
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

    public void ShowMessage(string text)
    {
        CancelInvoke();
        errorText = text;
        InvokeRepeating("EditMessage", 0f, 2f);
    }

    public void EditMessage() 
    { 
        if(errorTxt.text == errorText)
        {
            errorTxt.text = "";
            CancelInvoke();
            return;
        }
        errorTxt.text = errorText;
    }

    public void UpdateUI()
    {
        UpdateLives();
        UpdateMoney();
        UpdateRoundNR();
        UpdateEnemiesLeft();
    }
}

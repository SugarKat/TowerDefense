using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConnectionMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject connectionMenu;
    public GameObject loginMenu;
    public TextMeshProUGUI message;

    public static ConnectionMenuManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void ConnectionSuccess()
    {
        connectionMenu.SetActive(false);
        loginMenu.SetActive(true);
    }
    public void ErrorMessage(string msg)
    {
        message.text = msg;
    }
    public void ConnectionMenu()
    {
        mainMenu.SetActive(false);
        connectionMenu.SetActive(true);
    }
}

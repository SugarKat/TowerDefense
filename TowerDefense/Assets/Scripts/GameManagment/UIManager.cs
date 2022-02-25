using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject connectionToServer;
    public GameObject serverToServer;
    public TextMeshProUGUI message;

    public static UIManager instance;

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
        connectionToServer.SetActive(false);
        serverToServer.SetActive(true);
    }
    public void ErrorMessage(string msg)
    {
        message.text = msg;
    }
}

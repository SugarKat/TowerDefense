using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Microsoft.AspNet.SignalR.Client;
using TMPro;
using System;

public class NetworkManager : MonoBehaviour
{
    HubConnection hubConnection = null;

    private IHubProxy proxy = null;

    public static NetworkManager instance { get; private set; }

    public Action<string> OnMessageReceive;
    private ServerConnector connector;

    private string comm;
    public TMP_InputField ip;
    public TMP_InputField port;
    public TMP_InputField msg;
    public int clientID = -1;
    public TMP_InputField nickname;
    public TextMeshProUGUI connection;
    public TextMeshProUGUI response;
    public TextMeshProUGUI IDtext;
    public TMP_InputField nameInput;
    public TMP_InputField message;

    [HideInInspector]
    public string playerName;

    public bool host { get; private set; }
    public bool connected { get; private set; }
    bool updateUI = false;
    bool clientReady = false;
    int roomConnectionID = -1;

    public async Task Start()
    {
        connector = new ServerConnector();
        OnMessageReceive += updateMessageField;
        host = false;
        //await connector.InitAsync();
        //SendButton.onClick.AddListener(SendMessage);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    private void Update()
    {
        if (updateUI)
        {
            //response.ForceMeshUpdate();
            updateUI = false;
        }
    }
    public void AutoConnect()
    {
        ip.text = "192.168.0.146";
        ConnectToServer();
    }
    public async void ConnectToServer()
    {
        if (connected)
        {
            return;
        }
        string url;
        if (port.text == "")
        {
            url = "http://" + ip.text + ":30502";
            Debug.Log(url);
        }
        else
        {
            url = "http://" + ip.text + ":" + port.text;
            Debug.Log(url + "typed");
        }
        connector = new ServerConnector();
        connector.OnMessageReceived += updateMessageField;

        //await connector.InitAsync(url);
        hubConnection = new HubConnection(url);
        proxy = hubConnection.CreateHubProxy("testing");
        await StartConnectionAsync();
        if (!connected)
        {
            ConnectionMenuManager.instance.ErrorMessage("Couldnt connect");
            Debug.LogError("Couldnt connect");
            return;
        }
        ConnectionMenuManager.instance.ConnectionSuccess();
        //clientID = proxy.Invoke<int>("userConnected", nickname.text).Result;
        //proxy.On<string>("receiveMessage", (message) => updateMessageField(message));
        //connection.text = "Connected to: " + ip + ":" + port;
        //IDtext.text = "Connection ID: " + clientID;
    }
    public void Login()
    {
        if (proxy == null)
        {
            return;
        }
        playerName = nickname.text;
        clientID = proxy.Invoke<int>("userConnected", nickname.text).Result;
        proxy.On<string>("receiveMessage", (message) => updateMessageField(message));

        LevelLoading.instance.LoadLevel(1);
    }
    private async Task StartConnectionAsync()
    {
        await hubConnection.Start();
        hubConnection.Start().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error while connecting: " + task.Exception.GetBaseException());
                Debug.Log("quiting");
            }
            else
            {
                Debug.Log("Connected to Server. The Connection ID is: " + hubConnection.ConnectionId);
                connected = true;
            }
        }).Wait();
    }
    public void StartSignal()
    {
        if (host)
        {
            Debug.Log("starting game");
            proxy.Invoke("startGame", "start", roomConnectionID);
        }
    }
    public void NextWaveSignal(int waveID)
    {
        if (host)
        {
            string comm = $"wave;{waveID}";
            proxy.Invoke("command", comm, roomConnectionID);
        }
    }
    public void BuildSignal(int buildingID, float xPos, float yPos)
    {
        string comm = $"build;{buildingID};{xPos};{yPos}";
        proxy.Invoke("command", comm, roomConnectionID);
    }
    public void CreateRoom(string roomName)
    {
        roomConnectionID = proxy.Invoke<int>("createRoom", roomName, clientID).Result;
        host = true;
        LobbyMenuMangaer.instance.OpenRoom(String.Format(playerName + ';' + "null"));
    }
    public void ConnectToRoom(int roomID)
    {
        roomConnectionID = roomID;
        host = false;
        string roomInfo = proxy.Invoke<string>("connectToRoom", clientID, roomID).Result;
        LobbyMenuMangaer.instance.OpenRoom(roomInfo);
    }
    public void DisconnectFromRoom()
    {
        LobbyMenuMangaer.instance.OpenRoomsList();
        if (host)
        {
            DeleteRoom();
        }
        roomConnectionID = -1;
    }
    public void DeleteRoom()
    {
        if (roomConnectionID == -1)
        {
            Debug.LogError("Error occured when this was called, as there either wasnt changed in identification of connected room, or this was called in wrong conditions");
            return;
        }
        host = false;
        proxy.Invoke("deleteRoom", roomConnectionID);
    }
    public string GetRoomInfo()
    {
        string info = proxy.Invoke<string>("getRoomInfo", roomConnectionID).Result;
        return info;
    }
    //Sets if client is ready or not
    public void SetClientState(bool state)
    {
        clientReady = state;
    }
    public void SendCommand()
    {
        if (!connected)
            return;
        switch (comm)
        {
            /*case "admin":
                Console.Write("pass: ");
                string pass = Console.ReadLine();
                var b = hubProxy.Invoke<string>("grantAdminAcc", pass).Result;
                if (b == "1")
                {
                    hasAdmin = true;
                    Console.WriteLine("Admin granted");
                }
                else
                {
                    Console.WriteLine("Wrong password");
                }
                break;*/
            /*case "msg":
                Console.WriteLine("message: ");
                string msg = Console.ReadLine();
                hubProxy.Invoke<string>("messageServer", clientID, msg);
                break;*/
            case "check":
                response.text = (proxy.Invoke<bool>("isUpdated").Result ? "Server was updated" : "Server wasn't updated");
                break;
        }
    }
    public void SendWhisper()
    {
        if (!connected)
            return;
        proxy.Invoke("whisper", clientID, nameInput.text, message.text);
    }
    public void SendMessage()
    {
        if (!connected)
            return;
        proxy.Invoke<string>("messageServer", clientID, msg);
    }
    public void changeComm(string _comm)
    {
        if (!connected)
            return;
        comm = _comm;
    }
    private void updateMessageField(string message)
    {
        if (!connected)
            return;
        CommandInstantiator.instance.AddCommandToList(message);
    }
    public string RefreshRoomsList()
    {
        string list = proxy.Invoke<string>("getRoomList").Result;
        return list;
    }
    public void Disconnect()
    {
        connected = false;
        hubConnection.Stop();
        proxy = null;
        connection.text = "Not connected";
    }
    private void OnApplicationQuit()
    {
        if (hubConnection != null)
        {
            hubConnection.Stop();
        }
    }
}

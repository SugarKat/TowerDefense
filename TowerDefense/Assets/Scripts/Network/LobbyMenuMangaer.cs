using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyMenuMangaer : MonoBehaviour
{
    List<GameObject> listItems = new List<GameObject>();

    public GameObject lobbyListItem;
    public Transform lobbyListItemParent;

    [Space(1f)]
    public GameObject RoomsList;
    public GameObject RoomCreation;
    public GameObject Room;
    public TMP_InputField roomNameField;
    public TextMeshProUGUI roomHostName;
    public TextMeshProUGUI roomClientName;

    public static LobbyMenuMangaer instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }
    public void StartGame()
    {
        NetworkManager.instance.StartSignal();
    }
    public void OpenHostWindow()
    {
        RoomsList.SetActive(false);
        RoomCreation.SetActive(true);
        Room.SetActive(false);
    }
    public void OpenRoomsList()
    {
        RoomsList.SetActive(true);
        RoomCreation.SetActive(false);
        Room.SetActive(false);
    }
    public void OpenRoom(string roomInfo)
    {
        string[] Values = roomInfo.Split(';');
        roomHostName.text = Values[0];
        if (Values[1] == "null")
        {
            roomClientName.text = "";
        }
        else
        {
            roomClientName.text = Values[1];
        }

        RoomsList.SetActive(false);
        RoomCreation.SetActive(false);
        Room.SetActive(true);
    }
    public void ConnectToRoom()
    {
        string roomInfo = NetworkManager.instance.GetRoomInfo();
        OpenRoom(roomInfo);
    }
    public void CreateRoom()
    {
        NetworkManager.instance.CreateRoom(roomNameField.text);
    }
    public void RefreshList()
    {
        if (NetworkManager.instance == null)
        {
            Debug.LogError("Network manager not present");
            return;
        }
        ClearList();
        string list = NetworkManager.instance.RefreshRoomsList();

        if (list == "empty")
        {
            Debug.Log("no rooms");
            return;
        }
        string[] rooms = list.Split(':');
        foreach (string room in rooms)
        {
            string[] Values = room.Split(';');
            GameObject listItem = Instantiate(lobbyListItem);
            listItem.transform.SetParent(lobbyListItemParent);
            listItem.GetComponent<RoomListItem>().Setup(Values[1], Values[2], int.Parse(Values[0]));
            listItems.Add(listItem);
        }
    }
    public void ClearList()
    {
        for (int i = 0; i < listItems.Count; i++)
        {
            Destroy(listItems[i]);
        }
        listItems.Clear();
    }
}

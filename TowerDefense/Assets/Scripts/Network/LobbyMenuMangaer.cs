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
    public void OpenRoom()
    {
        RoomsList.SetActive(false);
        RoomCreation.SetActive(false);
        Room.SetActive(true);
    }
    public void CreateRoom()
    {

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

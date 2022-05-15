using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    public TextMeshProUGUI roomName;
    public TextMeshProUGUI hostName;

    int roomID = -1;

    public void Setup(string _name, string _host, int _roomID)
    {
        roomName.text = _name;
        hostName.text = _host;
        roomID = _roomID;
    }
    public void SelectRoom()
    {
        NetworkManager.instance.ConnectToRoom(roomID);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomGetter : MonoBehaviour
{
    public Text roomName;
    public LobbyMGR manager;

    public void Start()
    {
        manager = FindObjectOfType<LobbyMGR>();
    }

    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }
    public void OnClickItem()
    {
        manager.JoinRoom(roomName.text);
    }
}

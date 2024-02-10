using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyMGR : MonoBehaviourPunCallbacks
{
    public InputField roomInputField;
    public GameObject lobbyPanel, roomPanel;
    public playerData PlayerMGR;
    public Text roomName;
    public RoomGetter roomBttnPrefab;
    List<RoomGetter> roomBttnList = new List<RoomGetter>();
    public Transform contentObject;

    public float waitBetweenUpdates = 1.5f;
    float nextUpdateTime;
    public int GM, Map;
    public bool gameStarted;

    public GameObject playBttn;
    public GameObject[] players;
 
    void Start()
    {
        PhotonNetwork.JoinLobby();
    }
    void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            playBttn.SetActive(true);
        }
        else
        {
            playBttn.SetActive(false);
        }
    }

    public void createLobby()
    {
        if (roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() { MaxPlayers = 5 });

        }
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;

        PlayerMGR.spawnCharacter();
        PlayerMGR.setCaps();
        PlayerMGR.setFeet();
        PlayerMGR.setTorso();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + waitBetweenUpdates;
        }

        UpdateRoomList(roomList);
    }

    void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomGetter item in roomBttnList)
        {
            Destroy(item.gameObject);
        }
        roomBttnList.Clear();

        foreach (RoomInfo room in list)
        {
            RoomGetter newRoom = Instantiate(roomBttnPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomBttnList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public void SwampMap()
    {
        Map = 2;
    }
    public void EnclosureMap()
    {
        Map = 3;
    }
    public void TDM()
    {
        GM = 1;
    }
    public void CTF()
    {
        GM = 2;
    }
    public void onclickPlay()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(2);
    }
    public void mainMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }

}

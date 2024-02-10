using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class FFAMGR : MonoBehaviour
{
    int[] list;
    public GameObject[] players, cameras;
    public string pwon;
    public GameObject winpan, WinCamera;
    public Text wintxt;
    public AudioSource winNoise;
    public PhotonView view;
    public Photon.Realtime.Player playerwhowon;

    void Start()
    {
        WinCamera.SetActive(false);
        winpan.SetActive(false);
    }
    public void runWinOnALL(Photon.Realtime.Player pnum)
    {
        playerwhowon = pnum;
        view.RPC("PlayerKilled", RpcTarget.All);
    }
    [PunRPC]
    public void PlayerKilled()
    {
        players = GameObject.FindGameObjectsWithTag("Frog");
        foreach (GameObject player in players)
        {
            if (playerwhowon == player.GetComponent<PhotonView>().Owner)
            {
                player.GetComponent<PlayerMovement>().kills++;
                
                Debug.Log("kills: " + playerwhowon + player.GetComponent<PlayerMovement>().kills);
                if (player.GetComponent<PlayerMovement>().kills >= 20)
                {
                    pwon = playerwhowon.ToString();
                    view.RPC("win", RpcTarget.All);
                }
            }
        }
    }
    [PunRPC]
    void win()
    {
        Debug.Log(pwon);
        winpan.SetActive(true);
        wintxt.text = "winner: " + pwon;
        cameras = GameObject.FindGameObjectsWithTag("cam");
        foreach (GameObject player in cameras)
        {
            player.GetComponent<Camera>().enabled = false;
        }
        WinCamera.SetActive(true);
        StartCoroutine(Won());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    IEnumerator Won()
    {
        winNoise.Play();
        yield return new WaitForSeconds(4);
        view.RPC("winne", RpcTarget.All);
    }
    [PunRPC]
    void winne()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}


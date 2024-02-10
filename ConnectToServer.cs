using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public InputField usernameInput;
    public Button Play;
    public Text buttonText;
    public Animator cam, playeranim, thisanim;
    public MenuPlayer userInfo;
    public MenuSaveAndLoad SAL;
    public GameObject EnterName;

    public GameObject prevItem, prevItem2, newPlayer;

    void Start()
    {
        usernameInput.text = userInfo.user.playerTag;
        Debug.Log("start");
        if (usernameInput.text.Length <= 0)
        {
            Debug.Log("NoName");
            EnterName.SetActive(true);
            Play.interactable = false;
        }
        playeranim.SetBool("spawned", false);
    }
    public void PlayerSettings()
    {
        cam.SetBool("PlayerSettings", true);
        thisanim.SetBool("move", true);
        playeranim.SetBool("Move", true);
    }

    public void Back()
    {
        cam.SetBool("PlayerSettings", false);
        playeranim.SetBool("Move", false);
        playeranim.SetBool("spawned", false);
        thisanim.SetBool("move", true);
        if (usernameInput.text.Length >= 1)
        {
            EnterName.SetActive(false);
            Play.interactable = true;
            userInfo.user.playerTag = usernameInput.text;
        }
    }

    public void Settings()
    {
        cam.SetBool("Settings", true);
    }

    public void SettingsBack()
    {
        cam.SetBool("Settings", false);
    }

    public void Connect()
    {
        if(usernameInput.text.Length >= 1)
        {
            userInfo.user.playerTag = usernameInput.text;
            PhotonNetwork.NickName = usernameInput.text;
            SAL.save();
            buttonText.text = "connecting...";
            Debug.Log("Connecting...");
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void HatL()
    {        
        if (userInfo.user.caps > 0 && userInfo.user.caps <= 3)
        {
            userInfo.user.caps--;
            prevItem = GameObject.FindGameObjectWithTag("Hat");
            Destroy(prevItem);
            userInfo.setMenuHat();
        }
    }
    public void HatR()
    {
        if (userInfo.user.caps >= 0 && userInfo.user.caps < 3)
        {
            userInfo.user.caps++;
            prevItem = GameObject.FindGameObjectWithTag("Hat");
            Destroy(prevItem);
            userInfo.setMenuHat();
        }
    }
    public void TorsoL()
    {
        if (userInfo.user.torso > 0 && userInfo.user.torso <= 3)
        {
            userInfo.user.torso--;
            prevItem = GameObject.FindGameObjectWithTag("mid");
            Destroy(prevItem);
            userInfo.setMenuTorso();
        }
    }
    public void TorsoR()
    {
        if (userInfo.user.torso >= 0 && userInfo.user.torso < 3)
        {
            userInfo.user.torso++;
            prevItem = GameObject.FindGameObjectWithTag("mid");
            Destroy(prevItem);
            userInfo.setMenuTorso();
        }
    }
    public void feetL()
    {
        if (userInfo.user.feet > 0 && userInfo.user.feet <= 3)
        {
            userInfo.user.feet--;
            prevItem = GameObject.FindGameObjectWithTag("shoeL");
            prevItem2 = GameObject.FindGameObjectWithTag("shoeR");
            Destroy(prevItem);
            Destroy(prevItem2);
            userInfo.setMenuFeet();
        }
    }
    public void feetR()
    {
        if (userInfo.user.feet >= 0 && userInfo.user.feet < 3)
        {
            userInfo.user.feet++;
            prevItem = GameObject.FindGameObjectWithTag("shoeL");
            prevItem2 = GameObject.FindGameObjectWithTag("shoeR");
            Destroy(prevItem);
            Destroy(prevItem2);
            userInfo.setMenuFeet();
        }
    }
    public void characterL()
    {
        if (userInfo.user.character > 0 && userInfo.user.character <= 3)
        {
            userInfo.user.character--;
            prevItem = GameObject.FindGameObjectWithTag("Frog");
            Destroy(prevItem);
            userInfo.setMenuCharacter();
            playeranim.SetBool("spawned", true);
        }
    }
    public void characterR()
    {
        if (userInfo.user.character >= 0 && userInfo.user.character < 3)
        {
            userInfo.user.character++;
            prevItem = GameObject.FindGameObjectWithTag("Frog");
            Destroy(prevItem);
            userInfo.setMenuCharacter();
            playeranim.SetBool("spawned", true);
        }
    }


    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
    
    public void quit()
    {
        Application.Quit();
    }

}

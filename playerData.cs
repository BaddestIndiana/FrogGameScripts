using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

[Serializable]
public class Data
{
    public string playerTag;
    public int kills, skin, character, caps, feet, torso;

    public Data(string playerTag, int kills, int skin, int character, int caps, int feet, int torso)
    {
        this.playerTag = playerTag;
        this.kills = kills;
        this.skin = skin;
        this.character = character;
        this.caps = caps;
        this.feet = feet;
        this.torso = torso;
    }
}


public class playerData : MonoBehaviour
{
    public Data user;
    public SaveAndLoad SAL;
    public bool lobby;
    public GameObject frog1, frog2, frog3;
    public GameObject topHat, Cap, CowBoyHat, timmsBootsL, timmsBootsR, SneakersL, SneakersR, SandelsL, SandelsR, Chain, Scarf, Lanny, Head, FootL, FootR, Torso;
    public GameObject sH, sT, sFL, sFR;
    PhotonView view;


    public void Start()
    {
        if (lobby)
        {
            spawnCharacter();
            setCaps();
            setFeet();
            setTorso();
        }
    }

    public void RefreshAtros()
    {
        SAL.user = this.user;
    }
    public void spawnCharacter()
    {
        switch (user.character)
        {
            case 1:
                PhotonNetwork.Instantiate(frog1.name, gameObject.transform.position, transform.rotation);
                break;
            case 2:
                PhotonNetwork.Instantiate(frog2.name, gameObject.transform.position, transform.rotation);
                break;
            case 3:
                PhotonNetwork.Instantiate(frog3.name, gameObject.transform.position, transform.rotation);
                break;
            default:
                PhotonNetwork.Instantiate(frog1.name, gameObject.transform.position, transform.rotation);
                break;
        }
    }
    public void setCaps()
    {
        Head = GameObject.FindGameObjectWithTag("Head");
        switch (user.caps)
        {
            case 1:
                sH = PhotonNetwork.Instantiate(topHat.name, Head.transform.position, transform.rotation);
                sH.transform.parent = Head.gameObject.transform;
                break;
            case 2:
                sH = PhotonNetwork.Instantiate(Cap.name, Head.transform.position + new Vector3(0.0f, 0.2f, 0.0f), transform.rotation);
                sH.transform.parent = Head.gameObject.transform;
                break;
            case 3:
                sH = PhotonNetwork.Instantiate(CowBoyHat.name, Head.transform.position, transform.rotation);
                sH.transform.parent = Head.gameObject.transform;
                break;
            default:
                //noHat
                break;
        }
    }
    public void setFeet()
    {
        FootL = GameObject.FindGameObjectWithTag("FootL");
        FootR = GameObject.FindGameObjectWithTag("FootR");
        switch (user.feet)
        {
            case 1:
                sFL = PhotonNetwork.Instantiate(timmsBootsL.name, FootL.transform.position, transform.rotation);
                sFR = PhotonNetwork.Instantiate(timmsBootsR.name, FootR.transform.position, transform.rotation);
                sFL.transform.parent = FootL.gameObject.transform;
                sFR.transform.parent = FootR.gameObject.transform;
                break;
            case 2:
                sFL = PhotonNetwork.Instantiate(SneakersL.name, FootL.transform.position, transform.rotation);
                sFR = PhotonNetwork.Instantiate(SneakersR.name, FootR.transform.position, transform.rotation);
                sFL.transform.parent = FootL.gameObject.transform;
                sFR.transform.parent = FootR.gameObject.transform;
                break;
            case 3:
                sFL = PhotonNetwork.Instantiate(SandelsL.name, FootL.transform.position, transform.rotation);
                sFR = PhotonNetwork.Instantiate(SandelsR.name, FootR.transform.position, transform.rotation);
                sFL.transform.parent = FootL.gameObject.transform;
                sFR.transform.parent = FootR.gameObject.transform;
                break;
            default:
                //nofeet
                break;
        }
    }
    public void setTorso()
    {
        Torso = GameObject.FindGameObjectWithTag("Torso");
        switch (user.torso)
        {
            case 1:
                sT = PhotonNetwork.Instantiate(Chain.name, Torso.transform.position, transform.rotation);
                sT.transform.parent = Torso.gameObject.transform;
                break;
            case 2:
                sT = PhotonNetwork.Instantiate(Scarf.name, Torso.transform.position, transform.rotation);
                sT.transform.parent = Torso.gameObject.transform;
                break;
            case 3:
                sT = PhotonNetwork.Instantiate(Lanny.name, Torso.transform.position, transform.rotation);
                sT.transform.parent = Torso.gameObject.transform;
                break;
            default:
                //noTorso
                break;
        }
    }
}

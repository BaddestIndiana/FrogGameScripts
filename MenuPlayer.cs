using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MenuPlayer : MonoBehaviour
{
    public GameObject frog1, frog2, frog3, instantFrog, Hat1, Hat2, Hat3, Torso1, Torso2, Torso3, FeetL1, FeetL2, FeetL3, FeetR1, FeetR2, FeetR3;
    public GameObject Head, FootL, FootR, Torso, prevItem, EnviroObjects;
    public ConnectToServer CTS;
    public Data user;

    public MenuSaveAndLoad SAL;

    void Start()
    {
        setMenuCharacter();
    }
    public void setMenuCharacter()
    {
        prevItem = GameObject.FindGameObjectWithTag("Hat");
        Destroy(prevItem);
        prevItem = GameObject.FindGameObjectWithTag("mid");
        Destroy(prevItem);
        prevItem = GameObject.FindGameObjectWithTag("shoeL");
        Destroy(prevItem);
        prevItem = GameObject.FindGameObjectWithTag("shoeR");
        Destroy(prevItem);

        switch (user.character)
        {
            case 1:
                GameObject newFrog = Instantiate(frog1, gameObject.transform.position, transform.rotation, EnviroObjects.transform) as GameObject;
                CTS.playeranim = newFrog.GetComponent<Animator>();
                break;
            case 2:
                GameObject newFrog2 = Instantiate(frog2, gameObject.transform.position, transform.rotation, EnviroObjects.transform) as GameObject;
                CTS.playeranim = newFrog2.GetComponent<Animator>();
                break;
            case 3:
                GameObject newFrog3 = Instantiate(frog3, gameObject.transform.position, transform.rotation, EnviroObjects.transform) as GameObject;
                CTS.playeranim = newFrog3.GetComponent<Animator>();
                break;
            default:
                GameObject instantFrog = Instantiate(frog1, gameObject.transform.position, transform.rotation, EnviroObjects.transform) as GameObject;
                CTS.playeranim = instantFrog.GetComponent<Animator>();
                break;
        }
        
        setMenuHat();
        setMenuTorso();
        setMenuFeet();
    }
    public void setMenuHat()
    {
        Head = GameObject.FindGameObjectWithTag("Head");
        switch (user.caps)
        {
            case 1:
                GameObject Hat11 = Instantiate(Hat1, Head.transform.position + new Vector3(0.0f, 0.3f, 0.0f), Head.transform.rotation, Head.transform) as GameObject;
                break;
            case 2:
                GameObject Hat12 = Instantiate(Hat2, Head.transform.position + new Vector3(0.0f, 0.2f, 0.0f), Head.transform.rotation, Head.transform) as GameObject;
                break;
            case 3:
                GameObject Hat13 = Instantiate(Hat3, Head.transform.position + new Vector3(0.0f, 0.0f, -0.5f), Head.transform.rotation, Head.transform) as GameObject;
                break;
            default:
                Debug.Log("no Hat");
                break;
        }
    }
    public void setMenuTorso()
    {
        Torso = GameObject.FindGameObjectWithTag("Torso");
        switch (user.torso)
        {
            case 1:
                GameObject Torso11 = Instantiate(Torso1, Torso.transform.position, Torso.transform.rotation, Torso.transform) as GameObject;
                break;
            case 2:
                GameObject Torso12 = Instantiate(Torso2, Torso.transform.position, Torso.transform.rotation, Torso.transform) as GameObject;
                break;
            case 3:
                GameObject Torso13 = Instantiate(Torso3, Torso.transform.position, Torso.transform.rotation, Torso.transform) as GameObject;
                break;
            default:
                Debug.Log("no Torso");
                break;
        }
    }
    public void setMenuFeet()
    {
        FootL = GameObject.FindGameObjectWithTag("FootL");
        FootR = GameObject.FindGameObjectWithTag("FootR");
        switch (user.feet)
        {
            case 1:
                GameObject FeetL11 = Instantiate(FeetL1, FootL.transform.position, transform.rotation, FootL.transform) as GameObject;
                GameObject FeetR11 = Instantiate(FeetR1, FootR.transform.position, transform.rotation, FootR.transform) as GameObject;
                break;
            case 2:
                GameObject FeetL12 = Instantiate(FeetL2, FootL.transform.position, transform.rotation, FootL.transform) as GameObject;
                GameObject FeetR12 = Instantiate(FeetR2, FootR.transform.position, transform.rotation, FootR.transform) as GameObject;
                break;
            case 3:
                GameObject FeetL13 = Instantiate(FeetL3, FootL.transform.position, transform.rotation, FootL.transform) as GameObject;
                GameObject FeetR13 = Instantiate(FeetR3, FootR.transform.position, transform.rotation, FootR.transform) as GameObject;
                break;
            default:
                //no feet
                Debug.Log("no feet");
                break;
        }
    }

        public void RefreshAtros()
    {
        SAL.user = this.user;
    }
}

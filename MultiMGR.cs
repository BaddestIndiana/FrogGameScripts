using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiMGR : MonoBehaviour
{
    public GameObject playerSpawner, loadpan;
    public Transform Spawn1, Spawn2, Spawn3, Spawn4, SpawnPos;
    int rand;
    public GameObject[] Lobbyplayers;

    void Start()
    {
        rand = Random.Range(1, 4);
        switch (rand)
        {
            case 1:
                SpawnPos = Spawn1;
                break;
            case 2:
                SpawnPos = Spawn2;
                break;
            case 3:
                SpawnPos = Spawn3;
                break;
            case 4:
                SpawnPos = Spawn4;
                break;
        }
        StartCoroutine(spawn());
    }
    IEnumerator spawn()
    {
        
        yield return new WaitForSeconds(0.5f);
        loadpan.SetActive(false);
        playerSpawner.transform.position = SpawnPos.position;
        playerSpawner.GetComponent<playerData>().spawnCharacter();
        playerSpawner.GetComponent<playerData>().setCaps();
        playerSpawner.GetComponent<playerData>().setFeet();
        playerSpawner.GetComponent<playerData>().setTorso();
    }
}

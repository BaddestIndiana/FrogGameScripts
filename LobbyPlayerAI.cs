using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LobbyPlayerAI : MonoBehaviour
{
    public NavMeshAgent player;
    int randX, randZ;
    void Start()
    {
        player = GetComponent<NavMeshAgent>();
        StartCoroutine(playermove());
    }

    IEnumerator playermove()
    {
        randX = Random.Range(-5, 5);
        randZ = Random.Range(-5, 5);
        player.destination = new Vector3(randX,0,randZ);
        yield return new WaitForSeconds(3);
        StartCoroutine(playermove());
    }
}

using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSelector : NetworkBehaviour
{
    [SerializeField]
    private List<GameObject> Players;

    /*
    [SerializeField]
    private GameObject monster;

    
    [SerializeField]
    private int monsterIndex;
    */

    //Singleton
    public static PlayerSelector Singleton;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    

    void Start()
    {
        //ChooseMonster();
    }

    [ServerRpc]
    public void GivePlayerServerRpc(ulong clientId)
    {
        int index = Choose();

        GameObject Player = Instantiate(Players[index]);

        RemovePlayer(index);

        NetworkObject network = Player.GetComponent<NetworkObject>();

        network.SpawnWithOwnership(clientId);

    }

    /*
    public void ChooseMonster()
    {
        monsterIndex = UnityEngine.Random.Range(0, Players.Count);

        Players[monsterIndex].GetComponent<ShapeShifterController>().alternativePrefab = monster;
    }
    */
    public int Choose()
    {
        int random = UnityEngine.Random.Range(0, Players.Count - 1);

        return random;
    }

    public void RemovePlayer(int index)
    {
        Players.RemoveAt(index);
    }
}

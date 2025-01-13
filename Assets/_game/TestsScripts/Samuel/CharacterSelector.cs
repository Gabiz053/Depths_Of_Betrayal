using System.Collections.Generic;
using QFSW.QC;
using Unity.Netcode;
using UnityEngine;

public class CharacterSelection : NetworkBehaviour
{
    [SerializeField] private CharacterComplete[] players = default;


    void Start()
    {
        int randomIndex = Random.Range(0, players.Length);

        SpawnPlayerServerRpc(randomIndex);
    }

    [ServerRpc]
    public void SpawnPlayerServerRpc(int playerIndex)
    {
        NetworkManager.Singleton.AddNetworkPrefab(players[playerIndex].DiverPrefab);
    }
}

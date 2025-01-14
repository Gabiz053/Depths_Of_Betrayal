
using QFSW.QC;
using Unity.Netcode;
using UnityEngine;

public class CharacterSelection : NetworkBehaviour
{
    [SerializeField] private CharacterComplete[] players = default;


    void Start()
    {
    }

    [ServerRpc]
    public void SpawnPlayerServerRpc(int playerIndex)
    {
        NetworkManager.Singleton.AddNetworkPrefab(players[playerIndex].DiverPrefab);
    }
}

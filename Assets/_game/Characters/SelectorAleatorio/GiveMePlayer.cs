using Unity.Netcode;
using UnityEngine;

public class GiveMePlayer : NetworkBehaviour
{
    void Start()
    {
        PlayerSelector selector = FindAnyObjectByType<PlayerSelector>();
        selector.GivePlayerServerRpc(NetworkManager.Singleton.LocalClientId);

        Destroy(gameObject);
    }


    
}

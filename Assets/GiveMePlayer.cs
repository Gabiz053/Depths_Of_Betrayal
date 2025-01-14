using Unity.Netcode;
using UnityEngine;

public class GiveMePlayer : NetworkBehaviour
{

    void Start()
    {
        PlayerSelector.Singleton.GivePlayerServerRpc(NetworkManager.Singleton.LocalClientId);

        Destroy(gameObject);
    }
}

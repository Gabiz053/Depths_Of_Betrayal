using Unity.Netcode;

public class GiveMePlayer : NetworkBehaviour
{
    void Start()
    {
        if (IsOwner)
        {

            PlayerSelector selector = FindAnyObjectByType<PlayerSelector>();
            selector.GivePlayerServerRpc(NetworkManager.Singleton.LocalClientId);

            DestroyServerRpc();
        }
    }

    [ServerRpc]
    public void DestroyServerRpc()
    {
        Destroy(gameObject);
    }
}

using Unity.Netcode;

public abstract class CollectableObjetc : NetworkBehaviour
{
    public abstract void pickedUp(Character c);

    [ServerRpc(RequireOwnership = false)]
    public void UpdatePositionServerRpc(UnityEngine.Vector3 position)
    {
        transform.position = position;
    }
}

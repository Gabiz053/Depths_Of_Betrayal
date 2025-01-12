using System;
using Unity.Netcode;
using Unity.Netcode.Components;
using  UnityEngine;


public abstract class CollectableObjetc : NetworkBehaviour, INetworkSerializable
{
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        throw new NotImplementedException();
    }

    public abstract void pickedUp(Character c);

    [ServerRpc]
    public void UpdatePositionServerRpc(Vector3 newPosition)
    {
        transform.GetComponent<NetworkTransform>().transform.position = newPosition;
    }
}

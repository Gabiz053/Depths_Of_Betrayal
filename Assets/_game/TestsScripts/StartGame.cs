using System;
using Unity.Netcode;
using Unity.Netcode.Components;
using  UnityEngine;


public abstract class StartGame : NetworkBehaviour, INetworkSerializable
{
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        throw new NotImplementedException();
    }

    public abstract void GameStart();

}
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class cosasOnline : NetworkBehaviour
{
    // para un tipo de valor normal
    private NetworkVariable<MiStruct> numero = new NetworkVariable<MiStruct>(
        new MiStruct
        {
            valor = 10,
            texto = "Hola"
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    // para algo creado por nosotros

    // se podria usar serverRPC y clienRPC

    // el sever para mandar mensajes de los clientes al server
    // el client para mandar mensajes del server a los clientes (se puede decidir a cual)
    public struct MiStruct : INetworkSerializable
    {
        public int valor;
        public string texto;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref valor);
            serializer.SerializeValue(ref texto);
        }
    }

    // con esto solo se hace algo con el valor cuando cambia
    public override void OnNetworkSpawn()
    {
        numero.OnValueChanged += (MiStruct previousValue, MiStruct newValue) =>
        {
            Debug.Log(OwnerClientId + " random: " + newValue.valor + " texto: " + newValue.texto);
        };
    }

    // Update is called once per frame
    private void Update()
    {
        if (!IsOwner || !IsSpawned) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            numero.Value = new MiStruct
            {
                valor = Random.Range(0, 100),
                texto = "Hola",
            };


        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            numero.Value = new MiStruct
            {
                valor = Random.Range(0, 100),
                texto = "Hola",
            };

            TestServerRpc(new ServerRpcParams());

        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            numero.Value = new MiStruct
            {
                valor = Random.Range(0, 100),
                texto = "Hola",
            };

            TestClientRpc(new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });


        }

    }

    [ServerRpc]
    private void TestServerRpc(ServerRpcParams serverRpcParams)
    {
        // Aqui se puede hacer algo con el valor
        Debug.Log("TestServer (solo el server ejecuta): " + OwnerClientId + "; " + serverRpcParams.Receive.SenderClientId);
    }

    [ClientRpc]
    private void TestClientRpc(ClientRpcParams clientRpcParams)
    {
        // Aqui se puede hacer algo con el valor
        Debug.Log("TestClient (solo clientes ejecutan): " + numero.Value.valor);
    }
}

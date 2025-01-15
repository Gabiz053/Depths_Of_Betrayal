using Unity.Netcode;
using UnityEngine;

public class PlayerAttributes : NetworkBehaviour
{
    // Clase que contiene los datos del jugador
    public struct PlayerData : INetworkSerializable
    {
        public ulong playerId;
        public int health;
        public string playerName;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref playerId);
            serializer.SerializeValue(ref health);
            serializer.SerializeValue(ref playerName);
        }
    }

    // NetworkVariable para sincronizar datos del jugador
    private NetworkVariable<PlayerData> playerAttributes = new NetworkVariable<PlayerData>(
        new PlayerData
        {
            playerId = 0,
            health = 100,
            playerName = "Default"
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        // Reaccionar cuando cambian los datos del jugador
        playerAttributes.OnValueChanged += (PlayerData previousValue, PlayerData newValue) =>
        {
            Debug.Log($"Datos actualizados: ID={newValue.playerId}, Nombre={newValue.playerName}, Salud={newValue.health}");
        };
    }

    private void Update()
    {
        if (!IsOwner || !IsSpawned) return;

        if (Input.GetKeyDown(KeyCode.M))
        {
            // Actualizar atributos del jugador con datos aleatorios para pruebas
            playerAttributes.Value = new PlayerData
            {
                playerId = OwnerClientId,
                health = Random.Range(50, 100),
                playerName = "Player_" + OwnerClientId
            };

            // Enviar los datos actualizados a todos los conectados
            BroadcastPlayerAttributesClientRpc(playerAttributes.Value);
        }
    }

    // RPC para enviar datos a todos los clientes
    [ClientRpc]
    private void BroadcastPlayerAttributesClientRpc(PlayerData data)
    {
        Debug.Log($"Broadcast recibido: ID={data.playerId}, Nombre={data.playerName}, Salud={data.health}");
    }
}

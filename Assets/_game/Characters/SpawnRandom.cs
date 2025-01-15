using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class RandomSpawnInDonut : NetworkBehaviour
{
    private const float MinRadius = 8f; // Radio interno del "donut"
    private const float MaxRadius = 20f; // Radio externo del "donut"

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // Solo ejecuta el teletransporte si es el propietario del objeto
        if (IsOwner)
        {
            // Llama a la coroutine para asegurarte de que el teletransporte se haga al final
            StartCoroutine(DelayedTeleport());
        }
    }

    // private void Update()
    // {
    //     if (!IsOwner) return;

    //     // Detecta si se presiona la tecla J para hacer el teletransporte manual
    //     if (Input.GetKeyDown(KeyCode.J))
    //     {
    //         TeleportToRandomLocation();
    //     }
    // }

    private IEnumerator DelayedTeleport()
    {
        // Hacemos que el teletransporte sea lo último, de manera que cualquier otro proceso en OnNetworkSpawn se ejecute primero
        yield return null;

        TeleportToRandomLocation();
    }

    private void TeleportToRandomLocation()
    {
        // Genera una posición aleatoria dentro del donut
        Vector3 randomPosition = GenerateRandomPositionInDonut(MinRadius, MaxRadius);
        transform.position = randomPosition;

        // Para asegurarte de que el cambio de posición se sincroniza
        if (IsOwner)
        {
            SubmitTeleportRequestServerRpc(randomPosition);
        }

        Debug.Log($"Jugador teletransportado a {randomPosition}");
    }

    private Vector3 GenerateRandomPositionInDonut(float minRadius, float maxRadius)
    {
        // Genera un ángulo aleatorio en radianes
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // Genera una distancia aleatoria entre los radios mínimo y máximo
        float radius = Mathf.Sqrt(Random.Range(minRadius * minRadius, maxRadius * maxRadius));

        // Calcula las coordenadas en 2D y fija Y a 0 (ajústalo si es necesario)
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // Devuelve la posición en 3D 
        //Spawnea en el lobby, en la posición y = 50f
        return new Vector3(x, 50f, z);
    }

    // RPC para enviar la nueva posición a los demás jugadores
    [ServerRpc]
    private void SubmitTeleportRequestServerRpc(Vector3 position)
    {
        transform.position = position;
    }
}

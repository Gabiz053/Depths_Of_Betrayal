using UnityEngine;
using Unity.Netcode;

public class RandomSpawnInDonut : NetworkBehaviour
{
    private const float MinRadius = 8f; // Radio interno del "donut"
    private const float MaxRadius = 20f; // Radio externo del "donut"

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            TeleportToRandomLocation();
        }
    }

    // private void Update()
    // {
    //     if (!IsOwner) return;

    //     // Detecta si se presiona la tecla J
    //     if (Input.GetKeyDown(KeyCode.J))
    //     {
    //         TeleportToRandomLocation();
    //     }
    // }

    private void TeleportToRandomLocation()
    {
        Vector3 randomPosition = GenerateRandomPositionInDonut(MinRadius, MaxRadius);
        transform.position = randomPosition;
        Debug.Log($"Jugador teletransportado a {randomPosition}");
    }

    private Vector3 GenerateRandomPositionInDonut(float minRadius, float maxRadius)
    {
        // Genera un ángulo aleatorio en radianes
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // Genera una distancia aleatoria entre los radios mínimo y máximo
        float radius = Mathf.Sqrt(Random.Range(minRadius * minRadius, maxRadius * maxRadius));

        // Calcula las coordenadas en 2D y fija Y a 0 (o ajusta si el terreno tiene altura)
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // Devuelve la posición en 3D (asume que Y es 0; ajústalo según sea necesario)
        return new Vector3(x, 0f, z);
    }
}

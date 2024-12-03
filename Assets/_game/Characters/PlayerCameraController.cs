using Unity.Netcode;
using UnityEngine;
using Unity.Cinemachine;

public class PlayerCameraController : NetworkBehaviour
{
    // Referencias a la cámara del jugador (Cinemachine) y al AudioListener
    [SerializeField] private CinemachineCamera playerCamera;  // Se usa CinemachineCamera para ser más explícito

    public override void OnNetworkSpawn()
    {
        // Comprobamos si las referencias están asignadas
        if (playerCamera == null)
        {
            Debug.LogError("PlayerCamera o AudioListener no están asignados correctamente en el Inspector.");
            return;  // Salimos del método si no están asignadas
        }

        if (IsOwner)
        {
            // Aumenta la prioridad de la cámara para enfocarla como principal
            playerCamera.Priority = 1;
        }
        else
        {
            // Baja la prioridad de la cámara para que no sea la principal
            playerCamera.Priority = 0;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;  // Importa UnityEngine.UI
using Unity.Netcode;

public class SessionManager : MonoBehaviour
{
    // Aquí ahora usamos el tipo Canvas en lugar de GameObject
    public Canvas canvas;

    private void Start()
    {
        // Escucha cuando un cliente se conecte
        NetworkManager.Singleton.OnClientConnectedCallback += OnSessionJoin;
    }

    private void OnDestroy()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= OnSessionJoin;
    }

    private void OnSessionJoin(ulong clientId)
    {
        // Si el cliente es el local, ocultamos el Canvas
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            canvas.enabled = false;  // Desactiva el Canvas
        }
    }
}

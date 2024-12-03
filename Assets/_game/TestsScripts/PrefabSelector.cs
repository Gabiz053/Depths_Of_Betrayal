using Unity.Netcode;
using UnityEngine;

public class PrefabSelector : MonoBehaviour
{
    public GameObject[] playerPrefabs; // Lista de prefabs disponibles para los jugadores.
    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.Singleton;

        // Selecciona un prefab aleatorio de la lista.
        int selectedPrefabIndex = Random.Range(0, playerPrefabs.Length);
        GameObject selectedPrefab = playerPrefabs[selectedPrefabIndex];

        // Cambia dinámicamente el prefab del jugador en el NetworkConfig.
        networkManager.NetworkConfig.PlayerPrefab = selectedPrefab;

        // Inicia el host, servidor o cliente después de cambiar el prefab.
        networkManager.StartHost(); // O usa StartServer() o StartClient() según tu caso.
    }
}



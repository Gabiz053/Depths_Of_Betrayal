using Unity.Netcode;
using UnityEngine;

public class StartScript : NetworkBehaviour
{
    public Menu1 menu;
    private bool clicked = false;
    public GameObject suelo;

    private NetworkVariable<bool> gameStarted = new NetworkVariable<bool>(
        false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<int> timer = new NetworkVariable<int>(
        600, // Tiempo inicial en segundos (10 minutos).
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    // private NetworkVariable<int> monstruo = new NetworkVariable<int>(
    //     0,
    //     NetworkVariableReadPermission.Everyone,
    //     NetworkVariableWritePermission.Server
    // );

    private float startTime; // Tiempo real en que comenzó el juego.
    private float nextUpdateTime = 0f; // Controla cuándo debe actualizarse el temporizador.

    public override void OnNetworkSpawn()
    {
        gameStarted.OnValueChanged += (bool previousValue, bool newValue) =>
        {
            if (newValue)
            {
                Debug.Log("Game started");
                suelo.SetActive(false);
                menu.MostrarMenuInsideGameInicial();

                if (IsServer)
                {
                    startTime = Time.time; // Almacena el tiempo en que el juego comenzó.
                }
            }
        };

        timer.OnValueChanged += (int previousValue, int newValue) =>
        {
            string formattedTime = FormatTime(newValue);
            Debug.Log($"Timer updated: {formattedTime}");

            if (newValue <= 0)
            {
                EndGame();
            }
        };

        // monstruo.OnValueChanged += (int previousValue, int newValue) =>
        // {
        //     Debug.Log($"Monstruo: {newValue}");
        //     if (!IsServer && COMOSEVEESTOMyId == newValue)
        //     {
        //         menu.MostrarMenuIsMonster();

        //     } else if (!IsServer && MyId != newValue) {
        //         menu.MostrarMenuIsPlayer();
        //     }
        // };
    }

    private void Update()
    {
        if (!IsOwner || !IsSpawned) return;

        if (clicked)
        {
            gameStarted.Value = true;
        }

        if (IsServer && gameStarted.Value)
        {
            float elapsedTime = Time.time - startTime;
            int remainingTime = Mathf.Max(600 - Mathf.FloorToInt(elapsedTime), 0); // Calcula tiempo restante.

            // Actualiza el temporizador cada 5 segundos.
            if (Time.time >= nextUpdateTime && timer.Value != remainingTime)
            {
                timer.Value = remainingTime;
                nextUpdateTime = Time.time + 5f; // Programa la próxima actualización en 5 segundos.
            }
        }
    }

    private void EndGame()
    {
        Debug.Log("Game over! Time has run out.");
        // Aquí puedes añadir lógica para finalizar el juego, como mostrar un menú de fin de partida.
        menu.MostrarMenuGameOverTimeout();
    }

    private string FormatTime(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return $"{minutes:D2}:{seconds:D2}";
    }

    public void Click()
    {
        clicked = true;

        // Si eres el player 4, habria que hacer aqui el reparto aleatorio del monstruo
        //monstruo=4;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Acceder a las escenas
using UnityEngine.Video; // Acceder a los videos

public class Menu1 : MonoBehaviour
{
    // Referencias a los Canvas
    public GameObject CanvasInitialMenu; // Canvas del menú principal
    public GameObject CanvasCreateGame; // Canvas del "Create Game"
    public GameObject CanvasStartGame; // Canvas de espera para iniciar la partida
    public GameObject CanvasJoinGame; // Canvas del "Join Game"
    public GameObject CanvasJoinWait; // Canvas de espera para unirse a una partida
    public GameObject CanvasInsideGame; // Canvas del "Inside Game"
    public GameObject CanvasSettings; // Canvas de opciones
    public GameObject CanvasSettingsInsideGame; // Canvas de ajustes dentro del juego
    public GameObject CanvasInterfaceInsideGame; // Canvas de pausa del juego
    public GameObject CanvasControlsGame; // Canvas de controles del juego

    // Animacion de intro
    public GameObject CanvasVideo;
    public VideoPlayer videoPlayer;

    // Control para pausar el juego
    private bool isInGame = false; // Control para saber si el jugador está dentro del juego
    private bool isInsideGameMenuOpen = false; // Control para el estado del menú Inside Game
    private bool isSettingsInsideGameOpen = false; // Control para el estado del menú de ajustes dentro del juego
    private bool isControlsMenuOpen = false; // Control para el estado del menú de controles
    private bool isButtonResume = false; // Control para saber si se presionó el botón de "Resume"

    void Start()
    {
        // Ensure the VideoPlayer component is assigned
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component is not assigned!");
        }
        MostrarMenuInicial();
        UpdateCursorState(); // Asegurarse de que el estado del cursor sea correcto al inicio
    }

    // Función para mostrar el canvas del menú principal
    public void MostrarMenuInicial()
    {
        CanvasInitialMenu.SetActive(true);
        CanvasCreateGame.SetActive(false);
        CanvasStartGame.SetActive(false);
        CanvasJoinGame.SetActive(false);
        CanvasJoinWait.SetActive(false);
        CanvasInsideGame.SetActive(false);
        CanvasSettings.SetActive(false);
        CanvasSettingsInsideGame.SetActive(false);
        CanvasInterfaceInsideGame.SetActive(false);
        CanvasControlsGame.SetActive(false);
        CanvasVideo.SetActive(false);
    }

    // Función para mostrar el canvas "Create Game"
    public void MostrarMenuCreateGame()
    {
        CanvasInitialMenu.SetActive(false);
        CanvasCreateGame.SetActive(true);
        CanvasStartGame.SetActive(false);
        CanvasJoinGame.SetActive(false);
        CanvasJoinWait.SetActive(false);
        CanvasInsideGame.SetActive(false);
        CanvasSettings.SetActive(false);
        CanvasSettingsInsideGame.SetActive(false);
        CanvasInterfaceInsideGame.SetActive(false);
        CanvasControlsGame.SetActive(false);
        CanvasVideo.SetActive(false);
    }

    // Función para mostrar el canvas "Start Game"
     public void MostrarMenuStartGame()
    {
        // Desactivar y activar los Canvases relevantes
        CanvasCreateGame.SetActive(false);
        CanvasVideo.SetActive(true);

        // Ensure the VideoPlayer component is assigned
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component is not assigned!");
            return;
        }

        // Reproducir el video y comenzar la corrutina
        videoPlayer.Prepare();
        StartCoroutine(WaitForVideoToEnd());
    }

    private System.Collections.IEnumerator WaitForVideoToEnd()
    {
        Debug.Log("Esperando video");
        // Asegúrate de que el video se haya cargado
        while (!videoPlayer.isPrepared)
        {
           yield return null; // Espera hasta que el video esté listo
        }

        // Reproducir el video
        videoPlayer.Play();

        // Esperar hasta que el video termine
        while (videoPlayer.isPlaying)
        {
            yield return null; // Espera cada frame mientras se reproduce
        }
        Debug.Log("Terminado video");
        // Cuando el video termine, desactivar el CanvasVideo y mostrar otros Canvases
        CanvasVideo.SetActive(false);
        CanvasInitialMenu.SetActive(false);
        CanvasStartGame.SetActive(true);
        CanvasJoinGame.SetActive(false);
        CanvasJoinWait.SetActive(false);
        CanvasInsideGame.SetActive(false);
        CanvasSettings.SetActive(false);
        CanvasSettingsInsideGame.SetActive(false);
        CanvasInterfaceInsideGame.SetActive(false);
        CanvasControlsGame.SetActive(false);
    }

    // Función para mostrar el canvas "Join Game"
    public void MostrarMenuJoinGame()
    {
        CanvasInitialMenu.SetActive(false);
        CanvasCreateGame.SetActive(false);
        CanvasStartGame.SetActive(false);
        CanvasJoinGame.SetActive(true);
        CanvasJoinWait.SetActive(false);
        CanvasInsideGame.SetActive(false);
        CanvasSettings.SetActive(false);
        CanvasSettingsInsideGame.SetActive(false);
        CanvasInterfaceInsideGame.SetActive(false);
        CanvasControlsGame.SetActive(false);
        CanvasVideo.SetActive(false);
    }

    // Función para mostrar el canvas "Join Wait"
    public void MostrarMenuJoinWait()
    {
        CanvasJoinGame.SetActive(false);
        CanvasVideo.SetActive(true);

        // Ensure the VideoPlayer component is assigned
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component is not assigned!");
            return;
        }

        videoPlayer.Prepare();
        StartCoroutine(WaitForVideoToEnd1());


    }

    private System.Collections.IEnumerator WaitForVideoToEnd1()
    {
        // Asegúrate de que el video se haya cargado
        while (!videoPlayer.isPrepared)
        {
           yield return null; // Espera hasta que el video esté listo
        }

        // Reproducir el video
        videoPlayer.Play();

        // Esperar hasta que el video termine
        while (videoPlayer.isPlaying)
        {
            yield return null; // Espera cada frame mientras se reproduce
        }
        // Cuando el video termine, desactivar el CanvasVideo y mostrar otros Canvases
        CanvasVideo.SetActive(false);
        CanvasInitialMenu.SetActive(false);
        CanvasCreateGame.SetActive(false);
        CanvasStartGame.SetActive(false);
        CanvasJoinWait.SetActive(true);
        CanvasInsideGame.SetActive(false);
        CanvasSettings.SetActive(false);
        CanvasSettingsInsideGame.SetActive(false);
        CanvasInterfaceInsideGame.SetActive(false);
        CanvasControlsGame.SetActive(false);
    }

    // Función para mostrar el canvas "Settings"
    public void MostrarMenuSettings()
    {
        CanvasInitialMenu.SetActive(false);
        CanvasCreateGame.SetActive(false);
        CanvasStartGame.SetActive(false);
        CanvasJoinGame.SetActive(false);
        CanvasJoinWait.SetActive(false);
        CanvasInsideGame.SetActive(false);
        CanvasSettings.SetActive(true);
        CanvasSettingsInsideGame.SetActive(false);
        CanvasInterfaceInsideGame.SetActive(false);
        CanvasControlsGame.SetActive(false);
        CanvasVideo.SetActive(false);
    }

    // Función para mostrar el canvas "Inside Game" inicialmente (sin mostrar menús)
    public void MostrarMenuInsideGameInicial()
    {
        isInGame = true; // Cambiar el estado a "dentro del juego"
        CanvasInitialMenu.SetActive(false);
        CanvasCreateGame.SetActive(false);
        CanvasStartGame.SetActive(false);
        CanvasJoinGame.SetActive(false);
        CanvasJoinWait.SetActive(false);
        CanvasInsideGame.SetActive(false); // No se muestra el menú al entrar al juego
        CanvasSettings.SetActive(false);
        CanvasSettingsInsideGame.SetActive(false);
        CanvasControlsGame.SetActive(false);
        CanvasInterfaceInsideGame.SetActive(true);
        isInsideGameMenuOpen = false;
        isSettingsInsideGameOpen = false;
        isControlsMenuOpen = false;
        CanvasVideo.SetActive(false);
        UpdateCursorState(); // Asegurar el estado del cursor
    }

    // Función para mostrar el canvas "Inside Game"
    public void MostrarMenuInsideGame()
    {
        CanvasInsideGame.SetActive(true); // Muestra el menú "Inside Game"
        CanvasSettingsInsideGame.SetActive(false); // Oculta el menú de ajustes dentro del juego
        CanvasControlsGame.SetActive(false); // Oculta el menú de controles
        isInsideGameMenuOpen = true;
        isSettingsInsideGameOpen = false;
        isControlsMenuOpen = false;
        CanvasVideo.SetActive(false);
        UpdateCursorState(); // Asegura el estado del cursor
    }

    // Función para mostrar el canvas "Settings" dentro del juego
    public void MostrarMenuSettingsInsideGame()
    {
        CanvasInsideGame.SetActive(false); // Oculta el menú "Inside Game"
        CanvasControlsGame.SetActive(false);
        CanvasSettingsInsideGame.SetActive(true); // Muestra el menú de ajustes
        isInsideGameMenuOpen = false;
        isSettingsInsideGameOpen = true;
        isControlsMenuOpen = false;
        UpdateCursorState();
    }

    // Función para mostrar el canvas "Controls" dentro del juego
    public void MostrarMenuControlsGame()
    {
        CanvasInsideGame.SetActive(false); // Oculta el menú "Inside Game"
        CanvasSettingsInsideGame.SetActive(false);
        CanvasControlsGame.SetActive(true); // Muestra el menú de controles
        isInsideGameMenuOpen = false;
        isSettingsInsideGameOpen = false;
        isControlsMenuOpen = true;
        UpdateCursorState();
    }

    // Método para volver de "ControlsGame" a "InsideGame"
    public void VolverAlMenuInsideGameDesdeControls()
    {
        CanvasControlsGame.SetActive(false); // Oculta el menú de controles
        CanvasInsideGame.SetActive(true); // Muestra el menú "Inside Game"
        isControlsMenuOpen = false;
        isInsideGameMenuOpen = true;
        UpdateCursorState(); // Asegura el estado del cursor
    }

    // Método para volver de "SettingsInsideGame" a "InsideGame"
    public void VolverAlMenuInsideGame()
    {
        CanvasSettingsInsideGame.SetActive(false); // Oculta el menú de ajustes dentro del juego
        CanvasControlsGame.SetActive(false);
        CanvasInsideGame.SetActive(true); // Muestra el menú "Inside Game"
        isSettingsInsideGameOpen = false;
        isInsideGameMenuOpen = true;
        UpdateCursorState(); // Asegura el estado del cursor
    }

    // Función para esconder los menús dentro del juego
    public void EsconderMenusDentroDelJuego()
    {
        CanvasInsideGame.SetActive(false);
        CanvasSettingsInsideGame.SetActive(false);
        CanvasControlsGame.SetActive(false);
        isInsideGameMenuOpen = false;
        isSettingsInsideGameOpen = false;
        isControlsMenuOpen = false;
        UpdateCursorState(); // Actualiza el estado del cursor
    }

    // Función para esconder los menús dentro del juego
    public void ResumeGame()
    {
        isButtonResume = true;
        EsconderMenusDentroDelJuego();
    }

    // Función para regresar al menú principal
    public void VolverAlMenu()
    {
        isInGame = false; // Cambiar el estado a "fuera del juego"
        EsconderMenusDentroDelJuego();
        MostrarMenuInicial();
        UpdateCursorState(); // Asegurar el estado del cursor
    }

    // Función para salir del juego
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aquí se cierra el juego");
    }

    void Update()
    {
        // Detectar si se presiona la tecla Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isButtonResume)
            {
                isButtonResume = false;
                MostrarMenuInsideGame(); // Mostrar el menú "Inside Game"
            }
            else if (isSettingsInsideGameOpen || isControlsMenuOpen)
            {
                // Si alguno de los menús está abierto, cierra ambos menús
                EsconderMenusDentroDelJuego();
            }
            else if (isInsideGameMenuOpen)
            {
                // Si el menú dentro del juego está abierto, cierra ese menú
                EsconderMenusDentroDelJuego();
            }
            else if (isInGame)
            {
                // Si el jugador está en el juego, abre el menú "Inside Game"
                MostrarMenuInsideGame();
            }
        }
    }

    // Función para actualizar el estado del cursor
    private void UpdateCursorState()
    {
        if (isInsideGameMenuOpen || isSettingsInsideGameOpen || isControlsMenuOpen )
        {
            Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
            Cursor.visible = true; // Hace visible el cursor
        }
        else
        {
            Cursor.lockState = isInGame ? CursorLockMode.Locked : CursorLockMode.None; // Bloquear cursor en el juego
            Cursor.visible = !isInGame; // Oculta el cursor si está en el juego
        }
    }
}

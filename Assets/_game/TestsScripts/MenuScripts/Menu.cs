using UnityEngine;

public class Menu : MonoBehaviour
{
    // Referencias a los Canvas
    public GameObject CanvasInitialMenu; // Canvas del menú principal
    public GameObject CanvasCreateGame; // Canvas del "Create Game"
    public GameObject CanvasJoinGame; // Canvas del "Join Game"
    public GameObject CanvasInsideGame; // Canvas del "Inside Game"
    public GameObject CanvasSettings; // Canvas de opciones
    public GameObject CanvasSettingsInsideGame; // Canvas de ajustes dentro del juego
    public GameObject CanvasInterfaceInsideGame; // Canvas de pausa del juego

    // Control para pausar el juego
    private bool isInGame = false; // Control para saber si el jugador está dentro del juego
    private bool isInsideGameMenuOpen = false; // Control para el estado del menú Inside Game
    private bool isSettingsInsideGameOpen = false; // Control para el estado del menú de ajustes dentro del juego
    private bool isButtonResume = false; // Control para saber si se presionó el botón de "Resume"

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MostrarMenuInicial();
        UpdateCursorState(); // Asegurarse de que el estado del cursor sea correcto al inicio
    }

    // Función para mostrar el canvas del menú principal
    public void MostrarMenuInicial()
    {
        CanvasInitialMenu.SetActive(true);
        CanvasCreateGame.SetActive(false);
        CanvasJoinGame.SetActive(false);
        CanvasInsideGame.SetActive(false);
        CanvasSettings.SetActive(false);
        CanvasSettingsInsideGame.SetActive(false);
        CanvasInterfaceInsideGame.SetActive(false);
    }

    // Función para mostrar el canvas "Create Game"
    public void MostrarMenuCreateGame()
    {
        CanvasInitialMenu.SetActive(false);
        CanvasCreateGame.SetActive(true);
        CanvasJoinGame.SetActive(false);
        CanvasInsideGame.SetActive(false);
        CanvasSettings.SetActive(false);
        CanvasSettingsInsideGame.SetActive(false);
        CanvasInterfaceInsideGame.SetActive(false);
    }

    // Función para mostrar el canvas "Join Game"
    public void MostrarMenuJoinGame()
    {
        CanvasInitialMenu.SetActive(false);
        CanvasCreateGame.SetActive(false);
        CanvasJoinGame.SetActive(true);
        CanvasInsideGame.SetActive(false);
        CanvasSettings.SetActive(false);
        CanvasSettingsInsideGame.SetActive(false);
        CanvasInterfaceInsideGame.SetActive(false);
    }

    // Función para mostrar el canvas "Settings"
    public void MostrarMenuSettings()
    {
        CanvasInitialMenu.SetActive(false);
        CanvasCreateGame.SetActive(false);
        CanvasJoinGame.SetActive(false);
        CanvasInsideGame.SetActive(false);
        CanvasSettings.SetActive(true);
        CanvasSettingsInsideGame.SetActive(false);
        CanvasInterfaceInsideGame.SetActive(false);
    }

    // Función para mostrar el canvas "Inside Game" inicialmente (sin mostrar menús)
    public void MostrarMenuInsideGameInicial()
    {
        isInGame = true; // Cambiar el estado a "dentro del juego"
        CanvasInitialMenu.SetActive(false);
        CanvasCreateGame.SetActive(false);
        CanvasJoinGame.SetActive(false);
        CanvasInsideGame.SetActive(false); // No se muestra el menú al entrar al juego
        CanvasSettings.SetActive(false);
        CanvasSettingsInsideGame.SetActive(false);
        CanvasInterfaceInsideGame.SetActive(true);
        isInsideGameMenuOpen = false;
        isSettingsInsideGameOpen = false;
        UpdateCursorState(); // Asegurar el estado del cursor
    }

    // Función para mostrar el canvas "Inside Game"
    public void MostrarMenuInsideGame()
    {
        CanvasInsideGame.SetActive(true); // Muestra el menú "Inside Game"
        CanvasSettingsInsideGame.SetActive(false); // Oculta el menú de ajustes dentro del juego
        isInsideGameMenuOpen = true;
        isSettingsInsideGameOpen = false;
        UpdateCursorState(); // Asegura el estado del cursor
    }

    // Función para mostrar el canvas "Settings" dentro del juego
    public void MostrarMenuSettingsInsideGame()
    {
        CanvasInsideGame.SetActive(false); // Oculta el menú "Inside Game"
        CanvasSettingsInsideGame.SetActive(true); // Muestra el menú de ajustes
        isInsideGameMenuOpen = false;
        isSettingsInsideGameOpen = true;
        UpdateCursorState();
    }

    // Método para volver de "SettingsInsideGame" a "InsideGame"
    public void VolverAlMenuInsideGame()
    {
        CanvasSettingsInsideGame.SetActive(false); // Oculta el menú de ajustes dentro del juego
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
        isInsideGameMenuOpen = false;
        isSettingsInsideGameOpen = false;
        UpdateCursorState(); // Actualiza el estado del cursor
    }

    // Función para esconder los menús dentro del juego
    public void ResumeGame()
    {
        isButtonResume = true;
        CanvasInsideGame.SetActive(false);
        CanvasSettingsInsideGame.SetActive(false);
        isInsideGameMenuOpen = false;
        isSettingsInsideGameOpen = false;
        UpdateCursorState(); // Actualiza el estado del cursor
    }

    // Función para regresar al menú principal
    public void VolverAlMenu()
    {
        isInGame = false; // Cambiar el estado a "fuera del juego"
        isInsideGameMenuOpen = false;
        isSettingsInsideGameOpen = false;
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
            if (isSettingsInsideGameOpen)
            {
                // Si el menú de ajustes dentro del juego está abierto, cierra ambos menús
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
        if (isInsideGameMenuOpen || isSettingsInsideGameOpen)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Acceder a las escenas

public class Menu : MonoBehaviour
{
    // Referencias a los Canvas
    public GameObject CanvasInitialMenu; // Canvas del menú principal
    public GameObject CanvasCreateGame; // Canvas del "Create Game"
    public GameObject CanvasJoinGame; // Canvas del "Join Game"

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MostrarMenuInicial();
    }

    // Función para mostrar el canvas del menú principal
    public void MostrarMenuInicial()
    {
        CanvasInitialMenu.SetActive(true);
        CanvasCreateGame.SetActive(false);
        CanvasJoinGame.SetActive(false);
    }

    // Función para mostrar el canvas "Create Game"
    public void MostrarMenuCreateGame()
    {
        CanvasInitialMenu.SetActive(false);
        CanvasCreateGame.SetActive(true);
        CanvasJoinGame.SetActive(false);
    }

    // Función para mostrar el canvas "Join Game"
    public void MostrarMenuJoinGame()
    {
        CanvasInitialMenu.SetActive(false);
        CanvasCreateGame.SetActive(false);
        CanvasJoinGame.SetActive(true);
    }

    // Función para esconder el canvas 
    public void EsconderMenus()
    {
        CanvasInitialMenu.SetActive(false);
        CanvasCreateGame.SetActive(false);
        CanvasJoinGame.SetActive(false);
    }

    // Función para regresar al menú principal
    public void VolverAlMenu()
    {
        MostrarMenuInicial();
    }

     // Función para salir del juego
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aquí se cierra el juego");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

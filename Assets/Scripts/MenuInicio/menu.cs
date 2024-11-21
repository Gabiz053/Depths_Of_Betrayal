using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Acceder a las escenas

public class NewEmptyCSharpScript : MonoBehaviour
{
    // Referencias a los Canvas
    public GameObject Canvas1; // Canvas del menú principal
    public GameObject Canvas2; // Canvas del "Create Game"
    public GameObject Canvas3; // Canvas del "Join Game"
    public void CreateGame()
    {
        Canvas1.SetActive(false); // Desactiva el canvas del menú principal
        Canvas2.SetActive(true); // Activa el canvas del "Create Game"
        Canvas3.SetActive(false);
    }

    public void JoinGame()
    {
        Canvas1.SetActive(false); // Desactiva el canvas del menú principal
        Canvas2.SetActive(false); // Activa el canvas del "Create Game"
        Canvas3.SetActive(true);
    }

    public void VolverAlMenu()
    {
        Canvas1.SetActive(true); // Desactiva el canvas del menú principal
        Canvas2.SetActive(false); // Activa el canvas del "Create Game"
        Canvas3.SetActive(false);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aqui se cierra el juego");
    }
}

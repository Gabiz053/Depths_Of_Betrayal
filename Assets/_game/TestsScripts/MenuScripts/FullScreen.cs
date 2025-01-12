using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FullScreen : MonoBehaviour
{
    public List<Toggle> toggles; // Lista de todos los Toggles que deben sincronizarse

    void Start()
    {
        // Configurar el estado inicial de los toggles basándose en el estado de pantalla completa
        foreach (Toggle toggle in toggles)
        {
            toggle.isOn = Screen.fullScreen;
            // Añadir un listener para detectar cambios en cada toggle
            toggle.onValueChanged.AddListener(ActivarPantallaCompleta);
        }
    }

    public void ActivarPantallaCompleta(bool isFullScreen)
    {
        // Cambiar el estado de pantalla completa
        Screen.fullScreen = isFullScreen;

        // Sincronizar todos los toggles
        foreach (Toggle toggle in toggles)
        {
            if (toggle.isOn != isFullScreen)
            {
                toggle.isOn = isFullScreen;
            }
        }
    }

    void OnDestroy()
    {
        // Limpiar los listeners para evitar errores
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.RemoveListener(ActivarPantallaCompleta);
        }
    }
}

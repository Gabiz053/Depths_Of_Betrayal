using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Brillo : MonoBehaviour
{
    public Slider slider1; // Primer slider de brillo
    public Slider slider2; // Segundo slider de brillo
    public float sliderValue; // Valor actual del brillo
    public List<Image> panelesBrillo; // Lista de paneles que se oscurecen para simular el brillo

    void Start()
    {
        // Obtener el valor del brillo guardado o establecer un valor predeterminado
        sliderValue = PlayerPrefs.GetFloat("brillo", 0.5f);

        // Configurar los sliders iniciales
        slider1.value = sliderValue;
        slider2.value = sliderValue;

        // Configurar los listeners para que se sincronicen
        slider1.onValueChanged.AddListener(delegate { ChangeSlider(slider1.value, true); });
        slider2.onValueChanged.AddListener(delegate { ChangeSlider(slider2.value, false); });

        // Configurar el color inicial de los paneles
        ActualizarPanelesBrillo(sliderValue);
    }

    public void ChangeSlider(float valor, bool desdeSlider1)
    {
        sliderValue = valor; // Actualizar el valor del slider
        PlayerPrefs.SetFloat("brillo", sliderValue); // Guardar el valor del brillo

        // Sincronizar el otro slider
        if (desdeSlider1)
        {
            slider2.value = valor; // Si el cambio viene de slider1, actualizar slider2
        }
        else
        {
            slider1.value = valor; // Si el cambio viene de slider2, actualizar slider1
        }

        // Actualizar el color de todos los paneles
        ActualizarPanelesBrillo(valor);
    }

    private void ActualizarPanelesBrillo(float valor)
    {
        foreach (Image panel in panelesBrillo)
        {
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, valor);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    [System.Serializable]
    public class SliderMutePair
    {
        public Slider slider;      // El slider asociado
        public Image imagenMute;  // La imagen de mute asociada
    }

    public List<SliderMutePair> sliderMutePairs; // Lista de pares Slider-ImagenMute
    public float sliderValue;

    void Start()
    {
        // Configurar todos los sliders al valor guardado o por defecto
        sliderValue = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        foreach (SliderMutePair pair in sliderMutePairs)
        {
            pair.slider.value = sliderValue;
            RevisarSiEstoyMute(pair); // Asegurarse de que las imágenes reflejen el estado inicial
        }
        AudioListener.volume = sliderValue;
    }

    // Función para cambiar el volumen de todos los sliders y sincronizarlos
    public void ChangeSlider(float valor)
    {
        // Actualizar el valor del volumen y sincronizar todos los sliders
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = sliderValue;

        foreach (SliderMutePair pair in sliderMutePairs)
        {
            if (pair.slider.value != sliderValue) // Evitar bucles innecesarios
            {
                pair.slider.value = sliderValue;
            }
            RevisarSiEstoyMute(pair); // Actualizar la imagen de mute de cada slider
        }
    }

    // Función para revisar si el slider está en mute
    public void RevisarSiEstoyMute(SliderMutePair pair)
    {
        // Mostrar u ocultar la imagen de mute según el valor del slider
        if (pair.slider.value == 0 && pair.imagenMute != null)
        {
            pair.imagenMute.enabled = true;
        }
        else if (pair.imagenMute != null)
        {
            pair.imagenMute.enabled = false;
        }
    }
}

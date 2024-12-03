using UnityEngine;
using Unity.Netcode;

public class DisableSceneAudioOnSpawn : NetworkBehaviour
{
    private const string SceneCameraTag = "SceneCamera";

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            DisableSceneCameraAudio();
            StopBackgroundMusic();
        }
    }

    private void DisableSceneCameraAudio()
    {
        GameObject sceneCameraObject = GameObject.FindGameObjectWithTag(SceneCameraTag);
        if (sceneCameraObject == null)
        {
            Debug.LogWarning($"No se encontró ninguna cámara en la escena con la etiqueta '{SceneCameraTag}'.");
            return;
        }

        AudioListener audioListener = sceneCameraObject.GetComponent<AudioListener>();
        if (audioListener == null)
        {
            Debug.LogWarning("La cámara de la escena no tiene un componente AudioListener.");
            return;
        }

        audioListener.enabled = false;
        Debug.Log("AudioListener de la cámara de la escena desactivado correctamente.");
    }

    private void StopBackgroundMusic()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopMusic();
            Debug.Log("La música de fondo ha sido detenida.");
        }
        else
        {
            Debug.LogWarning("No se encontró una instancia de AudioManager.");
        }
    }
}

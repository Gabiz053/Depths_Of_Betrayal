using UnityEngine;
using Unity.Netcode;
using Unity.Cinemachine;

public class ToggleCameraView : NetworkBehaviour
{
    [Header("Cinemachine Camera")]
    public CinemachineCamera virtualCamera;

    [Header("Follow Targets")]
    public Transform thirdPersonFollowTarget; // Punto detrás del personaje para tercera persona.
    public Transform firstPersonFollowTarget; // Punto en la cabeza o cercano para primera persona.

    [Header("Character Model")]
    public GameObject characterModel; // Referencia al modelo del personaje (objeto que contiene el modelo 3D).

    private bool isFirstPerson = false;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            InitializeCamera();
        }
    }

    private void Update()
    {
        if (!IsOwner) return;

        // Alternar vistas al pulsar la tecla K
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isFirstPerson)
            {
                ActivateThirdPersonView();
            }
            else
            {
                ActivateFirstPersonView();
            }
        }
    }

    private void InitializeCamera()
    {
        // Configura la cámara para comenzar en tercera persona
        ActivateThirdPersonView();
    }

    private void ActivateThirdPersonView()
    {
        isFirstPerson = false;

        // Asigna el target de seguimiento para tercera persona
        virtualCamera.Follow = thirdPersonFollowTarget;

        // Muestra el modelo del personaje en tercera persona
        if (characterModel != null)
        {
            characterModel.SetActive(true);
        }

        Debug.Log("Vista de tercera persona activada.");
    }

    private void ActivateFirstPersonView()
    {
        isFirstPerson = true;

        // Asigna el target de seguimiento para primera persona
        virtualCamera.Follow = firstPersonFollowTarget;

        // Oculta el modelo del personaje en primera persona
        if (characterModel != null)
        {
            characterModel.SetActive(false);
        }

        Debug.Log("Vista de primera persona activada.");
    }
}

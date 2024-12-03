using UnityEngine;
using Unity.Netcode; // Add this for Mirror networking.

public class ViewController : NetworkBehaviour
{
    [SerializeField]
    public float sensitivity = 2;



    [Header("Look Settings")]
    [Tooltip("Suavizado para el movimiento del jugador")]
    public float smoothing = 1.5f;

    public float lookSpeedX = 2f;  // Velocidad de rotación en el eje Y (izquierda/derecha)

    [Tooltip("Velocidad de rotación de la cámara para el movimiento vertical del ratón.")]
    public float lookSpeedY = 2f;  // Velocidad de rotación en el eje X (arriba/abajo)

    [Tooltip("Ángulo máximo de rotación de la cámara hacia arriba.")]
    public float upperLookLimit = -600f; // Límite superior para rotación vertical

    [Tooltip("Ángulo máximo de rotación de la cámara hacia abajo.")]
    public float lowerLookLimit = 600f;  // Límite inferior para rotación vertical

    [Header("Mouse Lock Settings")]
    [Tooltip("Habilitar o deshabilitar el bloqueo del ratón.")]
    public bool isMouseLocked = true; // Bloqueo de ratón (puedes cambiarlo desde el Inspector)

    Vector2 velocity;
    Vector2 frameVelocity;

    void Start()
    {
        // Verifica si el jugador es el propietario y si el objeto está instanciado correctamente
        if (!IsOwner || !IsSpawned) return;

        // Inicializa el estado del ratón según la variable isMouseLocked
        UpdateMouseLockState();
    }
    void Update()
    {
        // Verifica si el jugador es el propietario y si el objeto está instanciado correctamente
        if (!IsOwner || !IsSpawned) return;

        // Obtén la velocidad del ratón de manera suave.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotar la cámara arriba-abajo (sobre la rotación local de la cámara)
        float rotationX = -velocity.y; // Rotación vertical
        float rotationY = velocity.x;  // Rotación horizontal

        // Rotación de la cámara en ambos ejes
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);

        // Detectar si se presiona Escape para alternar el bloqueo del ratón
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMouseLock(); // Alternar bloqueo del ratón al presionar Escape
        }

        // Si la variable isMouseLocked ha cambiado en el Inspector, actualiza el estado del ratón
        if (isMouseLocked != Cursor.lockState.Equals(CursorLockMode.Locked))
        {
            UpdateMouseLockState(); // Actualizar el estado del ratón
        }
    }

    // Actualiza el estado del ratón basado en la variable isMouseLocked
    private void UpdateMouseLockState()
    {
        if (isMouseLocked)
        {
            Cursor.lockState = CursorLockMode.Locked; // Bloquear el ratón
            Cursor.visible = false; // Hacer invisible el ratón
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // Desbloquear el ratón
            Cursor.visible = true; // Hacer visible el ratón
        }
    }

    // Alternar el estado de bloqueo del ratón al presionar Escape
    private void ToggleMouseLock()
    {
        isMouseLocked = !isMouseLocked; // Alternar el valor de la variable
        UpdateMouseLockState(); // Actualizar el estado del ratón
    }
}

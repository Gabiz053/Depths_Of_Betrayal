using Unity.Netcode;
using UnityEngine;

public class MovementController : NetworkBehaviour
{
    private CharacterController characterController; // Controlador de personaje

    [Header("Movement Settings")]
    [Tooltip("Velocidad de movimiento horizontal del personaje.")]
    public float speed = 5f;       // Velocidad de movimiento horizontal

    [Tooltip("Multiplicador de velocidad de flotación o hundimiento.")]
    public float floatSpeed = 1f;  // Velocidad de flotación (movimiento vertical)

    [Tooltip("Multiplicador de velocidad de hundimiento rápido.")]
    public float sinkSpeed = 2f;   // Velocidad de hundimiento rápido

    [Tooltip("Velocidad de hundimiento lento cuando no se mueve.")]
    public float slowSinkSpeed = 0.5f; // Velocidad de hundimiento lento

    private void Start()
    {
        // Verifica si el jugador es el propietario del objeto y si está instanciado en la red
        if (!IsOwner || !IsSpawned) return;

        // Obtiene el componente CharacterController del objeto
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Verifica si el jugador es el propietario del objeto y si está instanciado en la red
        if (!IsOwner || !IsSpawned) return;

        // Llama al método para manejar el movimiento del personaje
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Obtener las entradas de movimiento horizontal (A/D o flechas) y vertical (W/S o flechas)
        float moveX = Input.GetAxis("Horizontal");  // Movimiento horizontal (izquierda/derecha)
        float moveZ = Input.GetAxis("Vertical");    // Movimiento vertical (adelante/atrás)

        // Calcular la dirección del movimiento horizontal usando las entradas y las direcciones del personaje
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Inicializar la variable de entrada vertical para controlar la flotación o hundimiento
        float verticalInput = 0f;

        // Controlar el movimiento vertical: flotación (hacia arriba) o hundimiento (hacia abajo)
        if (Input.GetKey(KeyCode.Space))
        {
            verticalInput = 1f * floatSpeed;  // Flotando hacia arriba con velocidad ajustable
            AudioManager.instance.playSFX(AudioManager.instance.swim);  // Reproducir sonido de natación
        }
        else if (Input.GetKey(KeyCode.LeftShift) && moveX == 0 && moveZ == 0)
        {
            verticalInput = -sinkSpeed;  // Hundimiento rápido cuando no se mueve
        }
        else if (moveX == 0 && moveZ == 0)
        {
            verticalInput = -slowSinkSpeed; // Hundimiento lento cuando el personaje no se mueve
        }

        // Si se mueve hacia adelante o lateralmente, no hay hundimiento por defecto
        if (moveZ > 0 || moveX != 0)
        {
            verticalInput = 0f; // No se hunde si el personaje se está moviendo
        }

        // Dirección vertical de movimiento (subir o hundirse)
        Vector3 verticalMovement = Vector3.up * verticalInput;

        // Si el jugador presiona "Shift" mientras se mueve, aumenta la velocidad de movimiento horizontal
        if (Input.GetKey(KeyCode.LeftShift) && (moveZ > 0 || moveX != 0))
        {
            move *= 2f;  // Aumenta la velocidad de movimiento cuando se presiona Shift
        }

        // Combina el movimiento horizontal y vertical para obtener el movimiento total
        Vector3 totalMovement = move + verticalMovement;

        // Mueve al personaje con el controlador de personaje, multiplicando por la velocidad y el deltaTime para que sea independiente del framerate
        characterController.Move(totalMovement * speed * Time.deltaTime);
    }
}
using UnityEngine;

public class MovementController : MonoBehaviour
{

    private CharacterController characterController; // Controlador de personaje

    public float speed;       // Velocidad de movimiento

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Obtener el input del jugador para movimiento horizontal
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Dirección de movimiento horizontal (izquierda/derecha, adelante/atrás)
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Variable para la velocidad vertical
        float verticalInput = 0f;

        // Si el jugador presiona la tecla de espacio, flota hacia arriba
        if (Input.GetKey(KeyCode.Space)) 
        {
            verticalInput = 1f;  // Flotando hacia arriba
            AudioManager.instance.playSFX(AudioManager.instance.swim);
        }
        // Si solo presiona Shift, se hunde más rápido pero no se mueve horizontalmente
        else if (Input.GetKey(KeyCode.LeftShift) && (moveX == 0 && moveZ == 0)) 
        {
            verticalInput = -2f;  // Hundimiento más rápido cuando no te mueves
        }
        // Si no se mueve (AFK), se hunde lentamente
        else if (moveX == 0 && moveZ == 0) 
        {
            verticalInput = -0.5f; // Hundimiento lento cuando el personaje no se mueve
        }
        // Si se mueve hacia adelante o lateralmente, el personaje no se hunde (salvo si se presiona Shift)
        else if (moveZ > 0 || moveX != 0) 
        {
            verticalInput = 0f;  // No se hunde al moverse hacia adelante o lateralmente
        }

        // Dirección vertical (subir o hundirse)
        Vector3 verticalMovement = Vector3.up * verticalInput;

        // Si el jugador presiona Shift mientras se mueve hacia adelante o hacia los lados, aumenta la velocidad de movimiento
        if (Input.GetKey(KeyCode.LeftShift) && (moveZ > 0 || moveX != 0)) 
        {
            move *= 2f;  // Multiplicamos la velocidad de movimiento por un factor para correr más rápido
        }

        // Combinamos el movimiento horizontal y vertical
        Vector3 totalMovement = move + verticalMovement;

        // Mover al personaje
        characterController.Move(totalMovement * speed * Time.deltaTime);
    }
}

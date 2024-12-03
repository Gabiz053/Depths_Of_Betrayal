using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class AnimationController : NetworkBehaviour
{
    private Animator animator;
    
    // Hashes de los parámetros de animación para mejorar el rendimiento
    private int isSwimmingHash;
    private int isSpeedingHash;

    // Variables para mantener el estado de las animaciones
    private bool isSwimming;
    private bool isSpeeding;
    private bool isMoving;
    private bool shiftPressed;

    // Variables que almacenan el estado de las teclas de movimiento
    private bool forwardPressed, backwardPressed, leftPressed, rightPressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Verifica si el jugador es el propietario y si el objeto ha sido instanciado correctamente
        if (!IsOwner || !IsSpawned) return;

        // Obtiene el componente Animator del GameObject
        animator = GetComponent<Animator>();
        
        // Asigna los hashes de los parámetros de animación para optimizar su uso
        isSwimmingHash = Animator.StringToHash("isSwimming");
        isSpeedingHash = Animator.StringToHash("isSpeeding");

        // Inicializa los estados de las animaciones basados en los valores actuales
        isSwimming = animator.GetBool(isSwimmingHash);
        isSpeeding = animator.GetBool(isSpeedingHash);
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica si el jugador es el propietario y si el objeto está instanciado correctamente
        if (!IsOwner || !IsSpawned) return;

        // Maneja las entradas del jugador (teclas de movimiento)
        HandleInput();

        // Verifica si el jugador está moviéndose basándose en las teclas presionadas
        isMoving = forwardPressed || backwardPressed || leftPressed || rightPressed;

        // Controla el estado de las animaciones de natación y velocidad
        UpdateSwimmingState();
        UpdateSpeedingState();
    }

    // Maneja las entradas de las teclas para el movimiento y sprint
    private void HandleInput()
    {
        // Detecta las teclas de movimiento
        forwardPressed = Input.GetKey("w");
        backwardPressed = Input.GetKey("s");
        leftPressed = Input.GetKey("a");
        rightPressed = Input.GetKey("d");

        // Detecta si la tecla de sprint está presionada
        shiftPressed = Input.GetKey("left shift");
    }

    // Actualiza el estado de la animación de natación
    private void UpdateSwimmingState()
    {
        // Si el jugador está moviéndose y no está nadando, inicia la animación de natación
        if (!isSwimming && isMoving)
        {
            SetSwimmingState(true);
        }
        // Si el jugador no se está moviendo y está nadando, detiene la animación de natación
        else if (isSwimming && !isMoving)
        {
            SetSwimmingState(false);
        }
    }

    // Actualiza el estado de la animación de velocidad (sprint)
    private void UpdateSpeedingState()
    {
        // Si el jugador está moviéndose
        if (isMoving)
        {
            // Si está presionando shift y no está en modo sprint, activa la animación de velocidad
            if (shiftPressed && !isSpeeding)
            {
                SetSpeedingState(true);
            }
            // Si no está presionando shift y está en modo sprint, desactiva la animación de velocidad
            else if (!shiftPressed && isSpeeding)
            {
                SetSpeedingState(false);
            }
        }
        // Si el jugador no está moviéndose y está en modo sprint, desactiva la animación de velocidad
        else if (isSpeeding)
        {
            SetSpeedingState(false);
            SetSwimmingState(false); // También detiene la animación de natación cuando no se mueve
        }
    }

    // Establece el estado de la animación de natación
    private void SetSwimmingState(bool state)
    {
        // Solo actualiza el estado si es diferente al actual para evitar cambios innecesarios
        if (isSwimming != state)
        {
            animator.SetBool(isSwimmingHash, state);
            isSwimming = state;
        }
    }

    // Establece el estado de la animación de velocidad (sprint)
    private void SetSpeedingState(bool state)
    {
        // Solo actualiza el estado si es diferente al actual para evitar cambios innecesarios
        if (isSpeeding != state)
        {
            animator.SetBool(isSpeedingHash, state);
            isSpeeding = state;
        }
    }
}

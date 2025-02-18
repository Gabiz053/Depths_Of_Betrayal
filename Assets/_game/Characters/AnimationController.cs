using Unity.Netcode;
using UnityEngine;

public class AnimationController : NetworkBehaviour
{
    private Animator animator;
    
    // Hashes de los parámetros de animación para mejorar el rendimiento
    private int isSwimmingHash;
    private int isSpeedingHash;
    private int isTakingHash;
    private int isDeadHash;
    private int isHitHash;


    // Variables para mantener el estado de las animaciones
    private bool isSwimming;
    private bool isSpeeding;
    private bool isTaking;
    private bool isDead;
    private bool isHit;
    private bool isMoving;
    private bool shiftPressed;
    private bool takePressed;
    private bool deadPressed;
    private bool hitPressed;


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
        isTakingHash = Animator.StringToHash("isTaking");
        isDeadHash = Animator.StringToHash("isDead");
        isHitHash = Animator.StringToHash("isHit");

        // Inicializa los estados de las animaciones basados en los valores actuales
        isSwimming = animator.GetBool(isSwimmingHash);
        isSpeeding = animator.GetBool(isSpeedingHash);
        isTaking = animator.GetBool(isTakingHash);
        isDead = animator.GetBool(isDeadHash);
        isHit = animator.GetBool(isHitHash);

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
        UpdateTakingState();
        UpdateDeadState();
        UpdateHitState();
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

        // Detecta si la teclas de acciones
        takePressed = Input.GetKey("e");
        deadPressed = Input.GetKey("f");
        hitPressed = Input.GetKey("g");
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

    private void UpdateTakingState()
    {
        if (takePressed)
        {
            SetTakingState(true);
        }
        else
        {
            SetTakingState(false);
        }
    }

    private void UpdateDeadState()
    {
        if (deadPressed)
        {
            SetDeadState(true);
        }
        else
        {
            SetDeadState(false);
        }
    }

    private void UpdateHitState()
    {
        if (hitPressed)
        {
            SetHitState(true);
        }
        else
        {
            SetHitState(false);
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

    private void SetTakingState(bool state)
    {
        // Solo actualiza el estado si es diferente al actual para evitar cambios innecesarios
        if (isTaking != state)
        {
            animator.SetBool(isTakingHash, state);
            isTaking = state;
        }
    }

    private void SetDeadState(bool state)
    {
        // Solo actualiza el estado si es diferente al actual para evitar cambios innecesarios
        if (isDead != state)
        {
            animator.SetBool(isDeadHash, state);
            isDead = state;
        }
    }

    private void SetHitState(bool state)
    {
        if (isHit != state)
        {
            animator.SetBool(isHitHash, state);
            isHit = state;
        }
    }
}

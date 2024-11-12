using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    CharacterController characterController;

    int isSwimmingHash;
    int isSpeedingHash;

    public float speed = 5.0f;
    public float maxRotationAngle = 1.0f; // Ángulo máximo de giro en grados por frame
    public float sprintMultiplier = 2.0f;
    public float verticalSpeed = 3.0f; // Velocidad de movimiento vertical
    public float descentSpeed = 0.001f;  // Velocidad de descenso lento


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        isSwimmingHash = Animator.StringToHash("isSwimming");
        isSpeedingHash = Animator.StringToHash("isSpeeding");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool shiftPressed = Input.GetKey("left shift");
        bool spacePressed = Input.GetKey(KeyCode.Space);
        bool ctrlPressed = Input.GetKey(KeyCode.LeftControl);

        bool isMoving = false;
        if (forwardPressed || backwardPressed || leftPressed || rightPressed)
        {
            isMoving = true;
        }

        Vector3 direction = Vector3.zero;

        if (forwardPressed) direction += transform.forward;
        if (backwardPressed) direction -= transform.forward;
        if (leftPressed) direction -= transform.right;
        if (rightPressed) direction += transform.right;

        if (spacePressed)
        {
            direction += Vector3.up * verticalSpeed;
        }
        else if (ctrlPressed)
        {
            direction -= Vector3.up * verticalSpeed;
        }
        else
        {
            direction -= Vector3.up * descentSpeed;
        }

        direction = direction.normalized;
        float currentSpeed = shiftPressed ? speed * sprintMultiplier : speed;
        characterController.Move(direction * currentSpeed * Time.deltaTime);

        // Nueva sección para rotación con teclas de flecha
        float rotationInput = 0.0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationInput = -0.1f; // Gira a la izquierda
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationInput = 0.1f; // Gira a la derecha
        }

        // Aplicar rotación al personaje
        if (rotationInput != 0.0f)
        {
            float rotationAmount = rotationInput * maxRotationAngle; // Usa maxRotationAngle como la cantidad a rotar
            transform.Rotate(0, rotationAmount, 0); // Aplica la rotación solo en el eje Y
        }

        // Animaciones
        bool isSwimming = animator.GetBool(isSwimmingHash);
        bool isSpeeding = animator.GetBool(isSpeedingHash);

        if (!isSwimming && isMoving)
        {
            animator.SetBool(isSwimmingHash, true);
        }
        else if (isSwimming && !isMoving)
        {
            animator.SetBool(isSwimmingHash, false);
        }

        if (!isSpeeding && (isMoving && shiftPressed))
        {
            animator.SetBool(isSpeedingHash, true);
        }
        else if (isSpeeding && (isMoving && !shiftPressed))
        {
            animator.SetBool(isSpeedingHash, false);
        }
    }
}
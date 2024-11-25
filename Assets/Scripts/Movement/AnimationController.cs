using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    int isSwimmingHash;
    int isSpeedingHash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

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

        bool isMoving = false;
        if (forwardPressed || backwardPressed || leftPressed || rightPressed)
        {
            isMoving = true;
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

        if (!isSpeeding && isMoving && shiftPressed)
        {
            animator.SetBool(isSpeedingHash, true);
        }
        if (isSpeeding && isMoving && !shiftPressed)
        {
            animator.SetBool(isSpeedingHash, false);
        }
        if (isSpeeding && !isMoving && !shiftPressed)
        {
            animator.SetBool(isSpeedingHash, false);
            animator.SetBool(isSwimmingHash, false);
        }
    }
}


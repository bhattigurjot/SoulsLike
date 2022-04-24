using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSB
{
    public class InputManager : MonoBehaviour
    {
        PlayerControls playerControls;
        PlayerLocomotion playerLocomotion;
        AnimatorManager animatorManager;

        public float horizontalInput;
        public float verticalInput;
        public float moveAmount;
        public bool b_input;

        public float cameraInputX;
        public float cameraInputY;

        Vector2 movementInput;
        Vector2 cameraInput;

        void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();
                playerControls.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

                playerControls.PlayerActions.B.performed += i => b_input = true;
                playerControls.PlayerActions.B.canceled += i => b_input = false;
            }

            playerControls.Enable();
        }

        void OnDisable()
        {
            playerControls.Disable();
        }

        void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        public void HandleAllInputs()
        {
            HandleMovementInput();
            HandleSprintingInput();
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;
            cameraInputX = cameraInput.x;
            cameraInputY = cameraInput.y;

            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
            animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
        }

        private void HandleSprintingInput()
        {
            playerLocomotion.isSprinting = b_input && (moveAmount > 0.5f);
        }
    }
}

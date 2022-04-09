using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSB
{
    public class InputManager : MonoBehaviour
    {
        PlayerControls playerControls;
        CameraHandler cameraHandler;

        public float horizontalInput;
        public float verticalInput;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        Vector2 movementInput;
        Vector2 cameraInput;

        void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();
                playerControls.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            playerControls.Enable();
        }

        void OnDisable()
        {
            playerControls.Disable();
        }

        void Awake()
        {
            cameraHandler = CameraHandler.singleton;
        }

        void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
            }
        }

        public void HandleAllInputs()
        {
            HandleMovementInput();
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
    }
}

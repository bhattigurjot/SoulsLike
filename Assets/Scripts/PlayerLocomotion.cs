using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSB
{
    public class PlayerLocomotion : MonoBehaviour
    {
        InputManager inputManager;
        Transform cameraObject;
        Vector3 moveDirection;

        [HideInInspector] public AnimatorManager animatorManager;

        public Rigidbody playerRigidbody;

        [Header("Movement Speed")]
        [SerializeField] float walkingSpeed = 1.5f;
        [SerializeField] float runningSpeed = 5f;
        [SerializeField] float sprintingSpeed = 7f;
        [SerializeField] float rotationSpeed = 10f;

        public bool isSprinting;

        void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerRigidbody = GetComponent<Rigidbody>();
            cameraObject = Camera.main.transform;

            animatorManager = GetComponentInChildren<AnimatorManager>();
            animatorManager.Initialize();
        }

        void Update()
        {

        }

        public void HandleAllMovement()
        {
            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            moveDirection = cameraObject.forward * inputManager.verticalInput;
            moveDirection += cameraObject.right * inputManager.horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0; // so that player does not go up

            float speed = walkingSpeed;

            if (isSprinting)
            {
                speed = sprintingSpeed;
            }
            else
            {
                if (inputManager.moveAmount > 0.5f)
                    speed = runningSpeed;
                else
                    speed = walkingSpeed;
            }

            // Update player's velovity
            playerRigidbody.velocity = moveDirection * speed;
        }

        private void HandleRotation()
        {
            Vector3 targetDirection = Vector3.zero;
            targetDirection = cameraObject.forward * inputManager.verticalInput;
            targetDirection += cameraObject.right * inputManager.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0f; // so that player does not go up

            // Keep the rotation of the player
            if (targetDirection == Vector3.zero)
                targetDirection = transform.forward;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Update player's rotation
            transform.rotation = playerRotation;
        }


    }
}

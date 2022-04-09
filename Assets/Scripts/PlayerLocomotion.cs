using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSB
{
    public class PlayerLocomotion : MonoBehaviour
    {
        InputManager inputHandler;
        Transform cameraObject;
        Vector3 moveDirection;

        [HideInInspector] public AnimatorHandler animatorHandler;

        public Rigidbody playerRigidbody;

        [Header("Stats")]
        [SerializeField] float movementSpeed = 5f;
        [SerializeField] float rotationSpeed = 10f;

        void Awake()
        {
            inputHandler = GetComponent<InputManager>();
            playerRigidbody = GetComponent<Rigidbody>();
            cameraObject = Camera.main.transform;

            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            animatorHandler.Initialize();
        }

        void Update()
        {
            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0f);

            if (animatorHandler.canRotate)
            {
                HandleRotation();
            }

        }

        public void HandleAllMovement()
        {
            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            moveDirection = cameraObject.forward * inputHandler.verticalInput;
            moveDirection += cameraObject.right * inputHandler.horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0; // so that player does not go up

            // Update player's velovity
            playerRigidbody.velocity = moveDirection * movementSpeed;
        }

        private void HandleRotation()
        {
            Vector3 targetDirection = Vector3.zero;
            targetDirection = cameraObject.forward * inputHandler.verticalInput;
            targetDirection += cameraObject.right * inputHandler.horizontalInput;
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

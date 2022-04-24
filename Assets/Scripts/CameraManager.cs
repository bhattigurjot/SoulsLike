using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSB
{
    public class CameraManager : MonoBehaviour
    {
        [Header("References")]
        private InputManager inputManager;
        [SerializeField] private Transform targetTransform;
        [SerializeField] private Transform cameraPivotTransform;
        [SerializeField] private Transform cameraTransform;

        [Header("Camera Settings")]
        [SerializeField] private float cameraLookSpeed = 0.2f;
        [SerializeField] private float cameraFollowSpeed = 0.1f;
        [SerializeField] private float cameraPivotSpeed = 0.3f;
        [SerializeField] private float minimumPivotAngle = -35f;
        [SerializeField] private float maximumPivotAngle = 35f;
        [SerializeField] private float cameraCollisionRadius = 0.2f;
        [SerializeField] private float cameraCollisionOffset = 0.2f;
        [SerializeField] private float minimumCollisionOffset = 0.2f;
        [SerializeField] private LayerMask collisionLayers;


        private Vector3 cameraTransformPosition;
        private Vector3 cameraFollowVelocity = Vector3.zero;

        private float defaultPosition;
        private float cameraLookAngle; // look up and down
        private float cameraPivotAngle; // look left and right
        

        void Awake()
        {
            inputManager = FindObjectOfType<InputManager>();
            cameraTransform = Camera.main.transform;
            defaultPosition = cameraTransform.localPosition.z;
        }

        public void HandleAllCameraMovement()
        {
            FollowTarget();
            RotateCamera();
            HandleCameraCollisions();
        }

        private void FollowTarget()
        {
            Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, Time.deltaTime * cameraFollowSpeed);
            transform.position = targetPosition;
        }

        private void RotateCamera()
        {
            Quaternion targetRotation;

            // Update camera's look and pivot angles
            cameraLookAngle += (inputManager.cameraInputX * cameraLookSpeed);
            cameraPivotAngle -= (inputManager.cameraInputY * cameraPivotSpeed);
            cameraPivotAngle = Mathf.Clamp(cameraPivotAngle, minimumPivotAngle, maximumPivotAngle);

            // Update camera rotation and pivot rotations
            targetRotation = Quaternion.Euler(0f, cameraLookAngle, 0f);
            transform.rotation = targetRotation;
            // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * cameraLookSpeed);
            targetRotation = Quaternion.Euler(cameraPivotAngle, 0f, 0f);
            cameraPivotTransform.localRotation = targetRotation;
            // cameraPivotTransform.localRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * cameraLookSpeed);
        }

        private void HandleCameraCollisions()
        {
            float targetPosition = defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize();

            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
            {
                float distance = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetPosition = -(distance - cameraCollisionOffset);
            }

            if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
            {
                targetPosition -= minimumCollisionOffset;
            }

            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
            cameraTransform.localPosition = cameraTransformPosition;
        }    
    }
}
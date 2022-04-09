using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSB
{
    public class PlayerManager : MonoBehaviour
    {
        InputManager inputManager;
        PlayerLocomotion playerLocomotion;

        void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        void Update()
        {
            inputManager.HandleAllInputs();
        }

        void FixedUpdate()
        {
            playerLocomotion.HandleAllMovement();
        }
    }
}

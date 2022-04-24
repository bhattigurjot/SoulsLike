using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSB
{
    public class AnimatorManager : MonoBehaviour
    {
        private Animator anim;
        private int vertical;
        private int horizontal;

        public void Initialize()
        {
            anim = GetComponentInChildren<Animator>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            float v = 0f;
            float h = 0f;

            #region Vertical
            if (verticalMovement > 0 && verticalMovement < 0.5f)
            {
                v = 0.55f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1f;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.5f)
            {
                v = -0.55f;
            }
            else if (verticalMovement < -0.55f)
            {
                v = -1f;
            }
            else 
            {
                v = 0f;
            }
            #endregion

            #region Horizontal
            if (horizontalMovement > 0 && horizontalMovement < 0.5f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1f;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.5f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.5f)
            {
                h = -1f;
            }
            else 
            {
                h = 0f;
            }
            #endregion

            if (isSprinting)
            {
                h = horizontalMovement;
                v = 2f;
            }

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

    }
}
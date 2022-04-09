using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSB
{
    public class AnimatorHandler : MonoBehaviour
    {
        public Animator anim;
        int vertical;
        int horizontal;
        public bool canRotate;

        public void Initialize()
        {
            anim = GetComponent<Animator>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
        {
            #region Vertical
            float v = 0f;
            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.55f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1f;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
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
            float h = 0f;
            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.55f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1f;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.55f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1f;
            }
            else 
            {
                h = 0f;
            }
            #endregion

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void StopRotation()
        {
            canRotate = false;
        }
    }
}
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        public bool m_Spear;
        public float f = 200f;
        public bool canMove = true;
        public int SpearCounter;
        

        private void Start()
        {
            f = 200f;
            SpearCounter = 0;
        }

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
           
            }

            if (!m_Spear&&SpearCounter>0)
            {

                if (CrossPlatformInputManager.GetButton("Spear"))
                {
                    if (f < 1500f)
                        f += 10f;
                    canMove = false;
                }
                if (CrossPlatformInputManager.GetButtonUp("Spear"))
                {
                    m_Spear = true;
                    canMove = true;
                    SpearCounter--;
                }
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
           
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            
            // Pass all parameters to the character control scrip
            m_Character.PlayerSkills(m_Spear,f);
            if (canMove)
                m_Character.Move(h, crouch, m_Jump);
            else
                m_Character.Move(0,false, m_Jump);
            m_Jump = false;
        }
    }
}

using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        public GameObject m_WhatIsSpear;
        GameObject FlyingSpear;
        public bool canDoubleJump;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        int i = 0;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    
                }
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {


            if (coll.gameObject.tag == "Spear" && coll.gameObject.GetComponent<SpearScript>().iJustSpawned == false&& canDoubleJump)
            {
                Debug.Log("fuck me");
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000));
                canDoubleJump = false;
            
            }

            if (coll.gameObject.tag == "slimey")
            {
                Debug.Log("screw me");
                gameObject.GetComponent<Platformer2DUserControl>().canMove = false;
                m_Anim.SetFloat("Speed", 0);
                if (m_FacingRight)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (10, 0);
                }

                if (!m_FacingRight)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
                }
            }

            if (coll.gameObject.tag == "SpearPickup")
            {
                gameObject.GetComponent<Platformer2DUserControl>().SpearCounter++;
                Destroy(coll.gameObject);
            }
        }

        private void OnCollisionExit2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "slimey")
            {
                gameObject.GetComponent<Platformer2DUserControl>().canMove = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }

            if (coll.gameObject.tag == "Spear")
            {
                canDoubleJump = true;
            }
        }

        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }

        public void PlayerSkills(bool Spear, float f)
        {
            if (Spear)
            {

                if (m_FacingRight)
                {
                    FlyingSpear = Instantiate(m_WhatIsSpear, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                    FlyingSpear.GetComponent<Rigidbody2D>().AddForce(new Vector2(f, 80));
                    
                }
                else
                {
                    FlyingSpear = Instantiate(m_WhatIsSpear, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                    Vector3 theScale = FlyingSpear.transform.localScale;
                    theScale.x *= -1;
                    FlyingSpear.transform.localScale = theScale;
                    FlyingSpear.GetComponent<Rigidbody2D>().AddForce(new Vector2(f*-1f, 80));
                    
                }
                gameObject.GetComponent<Platformer2DUserControl>().f = 200f;
                gameObject.GetComponent<Platformer2DUserControl>().m_Spear = false;
                Destroy(FlyingSpear, 5);
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}

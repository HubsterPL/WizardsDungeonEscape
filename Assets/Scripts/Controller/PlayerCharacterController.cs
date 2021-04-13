using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controller
{
    public class PlayerCharacterController : MonoBehaviour
    {
        [SerializeField] Collider2D collider;
        [SerializeField] Rigidbody2D rb2d;
        [Header("Physcial Perception Settings")]
        [SerializeField] [Range(2, 10)] int groundCheckRayCount = 3;
        [SerializeField] LayerMask groundCheckMask;
        [SerializeField] float groundCheckDistance = 1f;
        [SerializeField] float skinWidth = .1f;
        
        
        [Header("Movement Settings")]
        [SerializeField] float jumpSpeed = 10f;
        [SerializeField] float runSpeed = 5f;
        [Header("Midair Movement Settings")]
        [SerializeField] float midairUpGravityMulti = .7f;
        [SerializeField] float midairDownGravityMulti = 1.5f;
        [SerializeField] float midairStrifeAcceleration = 5f;
        [Header("States")]
        public bool isGrounded = false;

        float passedOneWayAt = -10f;
        [SerializeField] float passedOneWaySafetyCooldown = .25f;

        float jumpedAt = -10f;
        [SerializeField] float jumpGroundCheckCooldown = .25f;
        // Grounded movement global variables
        IVelocity groundVelocityReference;
        float groundYSpeedOld; // Used to handle for ground momentum changes on Y axis

        bool jumped = false;

        private void Start()
        {
            if (collider == null)
                collider = GetComponent<Collider2D>();
            if (rb2d == null)
                rb2d = GetComponent<Rigidbody2D>();

            
        }

        private void Update()
        {


            if (Input.GetAxisRaw("Vertical") < 0f)
            {
                Physics2D.IgnoreLayerCollision(gameObject.layer, 14, true);
                passedOneWayAt = Time.time;
            }

            if (Input.GetAxisRaw("Vertical") >= 0f)
                Physics2D.IgnoreLayerCollision(gameObject.layer, 14, false);

            if (Input.GetButtonDown("Jump"))
            {
                jumped = false;
            }

            CheckGround();

            if (isGrounded && Input.GetButton("Jump"))
            {
                JumpAction();
            }
            if (isGrounded)
            {
                GroundedMovement();
            }
            else
                MidAirHorizontalControl();
            
            MidAirVerticalControl();
        }


        public float CheckGround()
        {
            if (jumpGroundCheckCooldown > Time.time - jumpedAt)
                return float.MaxValue;
            Vector2[] origins = GetGroundCheckOrigins();
            float distance = float.MaxValue;
            RaycastHit2D hit;
            foreach(var o in origins)
            {
                hit = Physics2D.Raycast(o, Vector2.down, groundCheckDistance, groundCheckMask);
                Debug.DrawRay(o, Vector3.down*groundCheckDistance);
                if (hit.transform != null && hit.distance < distance)
                {
                    distance = hit.distance;
                    groundVelocityReference = hit.collider.GetComponent<IVelocity>();
                }
            }
            if (distance <= groundCheckDistance)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
                groundVelocityReference = null;
            }

            return distance;
        }

        
        void GroundedMovement()
        {
            float targetXSpeed = Input.GetAxisRaw("Horizontal") * runSpeed;
            float groundYSpeedDelta = 0f;

            // Correction from moving ground
            if (groundVelocityReference != null)
            {
                targetXSpeed += groundVelocityReference.GetVelocity().x;
                groundYSpeedDelta = groundVelocityReference.GetVelocity().y - groundYSpeedOld;
                groundYSpeedOld = groundVelocityReference.GetVelocity().y;
                rb2d.velocity = new Vector2(targetXSpeed, groundVelocityReference.GetVelocity().y);
            }
            else
            {
                rb2d.velocity = new Vector2(targetXSpeed, rb2d.velocity.y);
            }

            //rb2d.velocity = new Vector2(targetXSpeed, rb2d.velocity.y + groundYSpeedDelta);
            
        }

        void MidAirVerticalControl()
        {
            float vinput = Input.GetAxisRaw("Vertical");
            if (vinput == -1)
                rb2d.gravityScale = midairDownGravityMulti;
            else if (vinput == 1)
                rb2d.gravityScale = midairUpGravityMulti;
            else
                rb2d.gravityScale = 1f;
        }
        
        void MidAirHorizontalControl()
        {
            float input = Input.GetAxisRaw("Horizontal");
            //if(input > 0f && rb2d.velocity.x < runSpeed || input < 0f && rb2d.velocity.x > -runSpeed)
            if(input != 0)
            {
                float delta = input * midairStrifeAcceleration * Time.deltaTime;
                float accelerationCap = runSpeed - Math.Abs(rb2d.velocity.x);
                // If is accelerating
                if (delta * rb2d.velocity.x > 0f)
                {
                    // And is over the cap already
                    if (accelerationCap < 0f)
                        // Then cancel acceleration
                        return;
                    // Is going to breach speed limit, so cap
                    if (accelerationCap < Mathf.Abs(delta))
                        delta = delta > 0f ? accelerationCap : -accelerationCap;
                }
                // Do the deed
                rb2d.velocity += delta * Vector2.right;

            }
        }

        void JumpAction()
        {
            if (jumped == false)
            {
                if (rb2d.velocity.y < 0f)
                    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                else
                    rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y + jumpSpeed);
                jumped = true;

                jumpedAt = Time.time;
                isGrounded = false;
            }
        }

        Vector2[] GetGroundCheckOrigins()
        {
            Vector2 left = new Vector2(collider.bounds.min.x, collider.bounds.min.y+skinWidth);
            Vector2 right = new Vector2(collider.bounds.max.x, collider.bounds.min.y+skinWidth);

            Vector2[] origins = new Vector2[groundCheckRayCount];
            for(int i = 0; i < groundCheckRayCount; i++)
            {
                origins[i] = Vector2.Lerp(left, right, (float)i/(groundCheckRayCount-1f));
            }

            return origins;
        }

    }
}

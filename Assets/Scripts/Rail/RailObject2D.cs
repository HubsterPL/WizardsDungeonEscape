using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailObject2D : MonoBehaviour
{
    public Rigidbody2D rb2d;

    public Rail2D rail;
    public bool isConnected;
    public bool counterBalanceRotation;
    public float comebackVelocityMultipiler = 5f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isConnected)
        {
            // Velocity Projection to ride the rail
            //rb2d.velocity = Vector2.Dot(rail.transform.up, rb2d.velocity.normalized) * rail.transform.up * rb2d.velocity.magnitude;
            rb2d.velocity = Vector2.Dot(rail.transform.up, rb2d.velocity) * rail.transform.up;

            // Adding velocity to shift platform back onto it's rail
            Vector2 closestPosOnRail = rail.transform.position + Vector2.Dot(rail.transform.up, transform.position - rail.transform.position) * rail.transform.up;
            Vector2 offsetToCorrect = (closestPosOnRail - (Vector2)transform.position);
            rb2d.velocity += offsetToCorrect.normalized * offsetToCorrect.sqrMagnitude * comebackVelocityMultipiler;

            // Rail length constraint

            float distanceFromRailCenter = Vector2.Dot(rail.transform.up, transform.position - rail.transform.position);

            
            if(distanceFromRailCenter > rail.Length / 2f)
            {
                if (rail.topNext != null)
                    rail = rail.topNext;
                else {
                    offsetToCorrect = (Vector2)transform.position - rail.GetTopPos();
                    rb2d.velocity -= offsetToCorrect.normalized * offsetToCorrect.sqrMagnitude * comebackVelocityMultipiler;
                }
            }else if (distanceFromRailCenter < -rail.Length / 2f)
            {
                if (rail.bottomNext != null)
                    rail = rail.bottomNext;
                else
                {
                    offsetToCorrect = (Vector2)transform.position - rail.GetBottomPos();
                    rb2d.velocity -= offsetToCorrect.normalized * offsetToCorrect.sqrMagnitude * comebackVelocityMultipiler;
                }
            }

        }
    }
}

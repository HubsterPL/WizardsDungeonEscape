using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class VelocityExpose : MonoBehaviour, IVelocity
{
    Rigidbody2D rb2d;
    public Vector2 GetVelocity()
    {
        return rb2d.velocity;
    }

    public void SetVelocity(Vector2 vector2)
    {
        rb2d.velocity = vector2;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
}

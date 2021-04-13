using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformIgnoreFix : MonoBehaviour
{
    public Collider2D platformEffectorCollider;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Physics2D.IgnoreCollision(platformEffectorCollider, other, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Physics2D.IgnoreCollision(platformEffectorCollider, other, false);
    }
}

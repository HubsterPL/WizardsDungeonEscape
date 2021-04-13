using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetableRigidbody : AResetable
{
    Vector3 position;
    float rotation;
    [SerializeField] Rigidbody2D rb2d;

    public override void RecordDefaults()
    {
        position = transform.position;
        rotation = transform.rotation.eulerAngles.z;
    }

    protected override void n_RestoreDefaults()
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        rb2d.velocity = Vector2.zero;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisInteractionTarget : AInteractive
{
    SpriteRenderer renderer;
    Rigidbody2D rb2d;

    float gravity;
    float linearDrag;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        gravity = rb2d.gravityScale;
        linearDrag = rb2d.drag;
    }

    public override void SelectInteraction()
    {
        renderer.material.SetFloat("_OutlineSize", 1f);
        renderer.material.SetColor("_OutlineColor", GameManager.Instance.Settings.outlineTelekinesis);
    }

    public override void UnselectInteraction()
    {
        renderer.material.SetFloat("_OutlineSize", 0f);
    }

    public void RestoreState()
    {
        rb2d.gravityScale = gravity;
        rb2d.drag = linearDrag;
        transform.gameObject.layer = 0;
    }
    public void TelekinesisState(float linearDragOverride)
    {
        gravity = rb2d.gravityScale;
        linearDrag = rb2d.drag;
        rb2d.gravityScale = 0;
        rb2d.drag = linearDragOverride;
        transform.gameObject.layer = 15;
    }

}

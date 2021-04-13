using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityInteraction : AInteractive
{
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] bool interactionInRange = false;

    public override void SelectInteraction()
    {
        if (interactionInRange)
        {
            renderer.material.SetFloat("_OutlineSize", 1f);
            renderer.material.SetColor("_OutlineColor", GameManager.Instance.Settings.outlineProximityInteractive);
        }
    }

    public override void UnselectInteraction()
    {
        renderer.material.SetFloat("_OutlineSize", 0f);
    }

    public virtual void Interact()
    {
        Debug.Log("No interaction");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            interactionInRange = true;
            PlayerTargetingSystem.AddProximityInteraction(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactionInRange = false;
            PlayerTargetingSystem.RemoveProximityInteraction(this);
        }
    }


}

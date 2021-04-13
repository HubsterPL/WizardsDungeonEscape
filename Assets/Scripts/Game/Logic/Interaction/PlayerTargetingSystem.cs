using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingSystem : MonoBehaviour
{
    public static PlayerTargetingSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    List<ProximityInteraction> proximityInteractives = new List<ProximityInteraction>();

    public ProximityInteraction ProximityTarget { get; private set; }
    public TelekinesisInteractionTarget TelekinesisTarget { get; private set; }

    public LayerMask telekinesisLayerMask;
    public Transform characterViewOrigin;

    public bool markForTelekinesisTarget = true;

    // Update is called once per frame
    void Update()
    {


        if (TelekinesisTarget != null && markForTelekinesisTarget)
            TelekinesisTarget.UnselectInteraction();
        TelekinesisTarget = null;

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float testDistance = (mouseWorldPos - (Vector2)characterViewOrigin.position).magnitude;
        var telekinesisObstacleTest = Physics2D.Raycast(characterViewOrigin.position, mouseWorldPos - (Vector2)characterViewOrigin.position, testDistance, telekinesisLayerMask);

        // Check if raycast hits nothing
        if (telekinesisObstacleTest.distance == 0f)
        {
            SelectTekekinesisTarget(mouseWorldPos);
        }

        // Handling Proximity Interactives Here
        SelectProximityTarget(mouseWorldPos);
    }

    private void SelectProximityTarget(Vector2 mouseWorldPos)
    {
        if (ProximityTarget != null)
            ProximityTarget.UnselectInteraction();
        ProximityTarget = null;
        {
            float maxDst = float.MaxValue;
            foreach (var i in proximityInteractives)
            {
                float dst = ((Vector2)i.transform.position - mouseWorldPos).magnitude;
                if (dst < maxDst)
                {
                    maxDst = dst;
                    ProximityTarget = i;
                }
            }

            if (ProximityTarget != null)
                ProximityTarget.SelectInteraction();
        }
    }

    private void SelectTekekinesisTarget(Vector2 mouseWorldPos)
    {
        List<TelekinesisInteractionTarget> telekinesisInteractives = new List<TelekinesisInteractionTarget>();

        RaycastHit2D[] hits = Physics2D.CircleCastAll(mouseWorldPos, 1f, Vector2.up, 0.01f);
        foreach (var hit in hits)
        {
            TelekinesisInteractionTarget telekinesisInteractive = hit.transform.GetComponent<TelekinesisInteractionTarget>();
            if (telekinesisInteractive == null)
                continue;

            telekinesisInteractives.Add(telekinesisInteractive);
        }

        {
            float maxDst = float.MaxValue;
            foreach (var i in telekinesisInteractives)
            {
                float dst = ((Vector2)i.transform.position - mouseWorldPos).magnitude;
                if (dst < maxDst)
                {
                    maxDst = dst;
                    TelekinesisTarget = i;
                }
            }

            if (TelekinesisTarget != null && markForTelekinesisTarget)
                TelekinesisTarget.SelectInteraction();
        }
    }

    public static void AddProximityInteraction(ProximityInteraction interaction)
    {
        Instance.proximityInteractives.Add(interaction);
    }

    public static void RemoveProximityInteraction(ProximityInteraction interaction)
    {
        try
        {
            Instance.proximityInteractives.Remove(interaction);
        }
        catch {
            Debug.LogError("There must have been a failure when adding interaction - nothing to remove.");
        }
    }
}

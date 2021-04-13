using UnityEngine;

public class MagicSkill : MonoBehaviour
{
    PlayerTargetingSystem interactionSystem;

    // Target Data
    Rigidbody2D target;
    FreezeState freeze;
    float linearDragSave;

    // Confiduration
    public float linearDragOverride = 10f;
    public float forceMulti = 300f;
    public int freezeManaCapacity = 10;

    public float minCost = 1f;
    public float maxDst = 10f;

    public bool debug = false;
    // Telekinesis Cost variable
    float costTickCounter = 0f;

    

    #region functions Unity
    void Start()
    {
        interactionSystem = PlayerTargetingSystem.Instance;
    }

    void Update()
    {
        // Start Telekinesis
        if (Input.GetButtonDown("Fire1"))
        {
            Log("Fire1");
            if(interactionSystem.TelekinesisTarget != null)
            {
                // Get Target, stop looking
                target = interactionSystem.TelekinesisTarget.GetComponent<Rigidbody2D>();
                interactionSystem.markForTelekinesisTarget = false;
                // Unfreeze If Frozen
                {
                    FreezeState freezeState = target.GetComponent<FreezeState>();
                    if (freezeState != null)
                    {
                        freezeState.Unfreeze();
                        Log("Unfreeze");
                    }
                }
                // Do Telekinesis thing
                AdjustRigidbodyForTelekinesis();

                costTickCounter = 0f;
                SetTelekinesisOutlineToTarget(1f, 1f);
            }
        }

        // Release Telekinesis
        if (Input.GetButtonUp("Fire1"))
        {
            Log("Released Telekinesis");
            if (target != null)
            {
                CancelTelekinesis();
            }
        }

        // Stop Freeze recharge
        if (Input.GetButtonUp("Fire2"))
        {
            Log("Released Freeze");
            ReleaseTarget();
        }

        // Get or Make Freeze
        if (Input.GetButtonDown("Fire2"))
        {
            Log("Pressed Freeze");
            TryGetTarget();

            if (target != null)
            {
                freeze = target.transform.gameObject.GetComponent<FreezeState>();
                if (freeze == null)
                {
                    if (Game.Score.GetMana() > 0)
                    {
                        freeze = ApplyFreezeToTarget();
                        ChargeFreeze(freeze);
                    }
                }
            }
        }

        // Charge Freeze
        if (Input.GetButton("Fire2"))
        {
            if (freeze != null)
            {
                if (freeze.GetMana() < freezeManaCapacity)
                {
                    ChargeFreeze(freeze);
                }
            }
        }

    }

    private void FixedUpdate()
    {
        // Telekinesis Maintance
        if (Input.GetButton("Fire1") && !Input.GetButton("Fire2"))
        {
            if (target != null)
            {
                float distance = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - target.position).magnitude;
                target.AddForce(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - target.position).normalized * Mathf.Clamp(distance, 0f, maxDst) * Time.deltaTime * forceMulti);
                costTickCounter += Time.deltaTime * Mathf.Clamp(distance, minCost, maxDst);

                if (costTickCounter > 1f)
                {
                    Game.Score.AddMana(-Mathf.FloorToInt(costTickCounter));

                    if (Game.Score.GetMana() == 0)
                        CancelTelekinesis();

                    costTickCounter = costTickCounter % 1f;
                }
                SetTelekinesisOutlineToTarget(1f, 1f);
            }
        }
    }

    #endregion

    #region functions public
    public void CancelTelekinesis()
    {
        Log("Telekinesis Cancel");
        if (target == null)
            return;

        SetTelekinesisOutlineToTarget(0f, 0f);
        RestoreRigidbody();
        ReleaseTarget();
    }

    
    #endregion

    #region functions private
    void AdjustRigidbodyForTelekinesis()
    {
        Log("Rigidbody Adjust");
        target.GetComponent<TelekinesisInteractionTarget>().TelekinesisState(linearDragOverride);
    }
    private void RestoreRigidbody()
    {
        Log("Rigidbody Restore");
        target.GetComponent<TelekinesisInteractionTarget>().RestoreState();
    }
    void ChargeFreeze(FreezeState freeze)
    {
        int freezeManaDelta = freezeManaCapacity - freeze.GetMana();
        if (freezeManaDelta > Game.Score.GetMana())
            freezeManaDelta = Game.Score.GetMana();
        freeze.AddManaCharge(freezeManaDelta);
        Game.Score.AddMana(-freezeManaDelta);

        Log("Charge Freeze: DELTA=" + freezeManaDelta.ToString());
    }
    FreezeState ApplyFreezeToTarget()
    {
        Log("Freeze Apply");
        return target.transform.gameObject.AddComponent<FreezeState>();
    }
    void ReleaseTarget()
    {
        if (target == null)
        {
            Log("Target Release Canceled: because no target");
            return;
        }

        Log("Target Release");
        target.GetComponent<TelekinesisInteractionTarget>().RestoreState();
        target = null;
        freeze = null;
        interactionSystem.markForTelekinesisTarget = true;
    }
    void TryGetTarget()
    {
        Log("Try Get Target");
        if (target == null)
        {
            if (interactionSystem.TelekinesisTarget != null)
            {
                target = interactionSystem.TelekinesisTarget.GetComponent<Rigidbody2D>();
                interactionSystem.markForTelekinesisTarget = false;
                Log("New Target");
                return;
            }
            Log("No Target");
        }

        Log("Already Has Target");

    }
    void SetTelekinesisOutlineToTarget(float width, float intensity)
    {
        SpriteRenderer renderer = target.GetComponent<SpriteRenderer>();
        renderer.material.SetColor("_OutlineColor", GameManager.Instance.Settings.outlineTelekinesisActive*intensity);
        renderer.material.SetFloat("_OutlineSize", width);
    }

    void Log(string s)
    {
        if (debug)
            Debug.Log(s);
    }
#endregion
}

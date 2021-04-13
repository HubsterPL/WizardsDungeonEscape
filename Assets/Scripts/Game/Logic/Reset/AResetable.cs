using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AResetable : MonoBehaviour
{
    public bool resetOnBarrier = true;
    public bool cancelControlOnReset = true;
    // Start is called before the first frame update
    void Awake()
    {
        RecordDefaults();
    }

    public abstract void RecordDefaults();

    /// <summary>
    /// Invoke what is inside by using Reset()
    /// </summary>
    protected abstract void n_RestoreDefaults();

    public void Reset()
    {
        n_RestoreDefaults();
        if(cancelControlOnReset)
        {
            GameManager.Instance.GetPlayer().GetTelekinesis().CancelTelekinesis();
            FreezeState freezeState = GetComponent<FreezeState>();
            if (freezeState != null)
                freezeState.Unfreeze();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (resetOnBarrier)
            if (collision.CompareTag("ResetObjectTrigger"))
            {
                Reset();
            }
    }
}

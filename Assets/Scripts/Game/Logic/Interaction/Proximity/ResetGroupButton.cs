using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGroupButton : ProximityInteraction
{
    // Childern will be searched for resetable objects
    [SerializeField]Transform groupParentTransform;

    public override void Interact()
    {
        AResetable[] targets = groupParentTransform.GetComponentsInChildren<AResetable>();
        foreach (var t in targets)
            t.Reset();
    }
}

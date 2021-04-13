using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSkill : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerTargetingSystem.Instance.ProximityTarget != null) {
            PlayerTargetingSystem.Instance.ProximityTarget.Interact();
        }
            
    }
}

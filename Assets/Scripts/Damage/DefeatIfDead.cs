using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatIfDead : MonoBehaviour
{
    void Start()
    {
        GetComponent<DamageController>().onDeath.AddListener(new UnityEngine.Events.UnityAction(LinkGameManagerDefeatEvent));
    }

    void LinkGameManagerDefeatEvent()
    {
        GameManager.Instance.Defeat();
    }

}

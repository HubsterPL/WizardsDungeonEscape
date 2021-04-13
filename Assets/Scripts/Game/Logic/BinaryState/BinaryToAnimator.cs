using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BinaryToAnimator : ABinarySwitch
{
    Animator animator;
    bool isOn;
    [SerializeField] string targetVariable = "IsOn";
    [SerializeField] bool reverseOnOff = false;
    [SerializeField] bool defaultState = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (defaultState)
            TurnOn();
        else
            TurnOff();

    }

    public override bool State()
    {
        return isOn;
    }

    public override void SwitchState()
    {
        if (isOn)
            TurnOff();
        else
            TurnOn();
    }

    public override void TurnOff()
    {

        isOn = reverseOnOff ? true : false;
        animator.SetBool(targetVariable, isOn);
    }

    public override void TurnOn()
    {
        isOn = reverseOnOff ? false : true;
        animator.SetBool(targetVariable, isOn);
    }
}

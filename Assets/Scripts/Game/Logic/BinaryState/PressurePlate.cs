using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PressurePlate : ABinarySwitch
{
    public List<ABinarySwitch> connectedComponents = new List<ABinarySwitch>();

    bool pressed = false;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override bool State()
    {
        return pressed;
    }

    public override void SwitchState()
    {
        if (pressed)
            TurnOff();
        else
            TurnOn();
    }

    public override void TurnOff()
    {
        pressed = false;
        animator.SetBool("IsOn", false);
        foreach (var b in connectedComponents)
            b.TurnOff();
    }

    public override void TurnOn()
    {
        pressed = true;
        animator.SetBool("IsOn", true);

        foreach (var b in connectedComponents)
            b.TurnOn();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TurnOn();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TurnOff();
    }
}

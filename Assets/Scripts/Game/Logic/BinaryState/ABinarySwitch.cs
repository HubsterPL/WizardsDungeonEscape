using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABinarySwitch : MonoBehaviour
{
    public abstract void TurnOn();
    public abstract void TurnOff();
    public abstract bool State();
    public abstract void SwitchState();
}

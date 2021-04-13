using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicalExtension : ABinarySwitch
{
    public enum CombineType
    {
        AND, OR
    }
    public CombineType combineType = CombineType.AND;

    public List<BinaryStateExpected> binaryStates = new List<BinaryStateExpected>();

    public List<ABinarySwitch> binariesAwaitingInput = new List<ABinarySwitch>();

    public float stateChangeDelay = 0f;

    public bool turnOffParadigmCheck = false;

    [SerializeField] bool state = false;
    bool stateChangeTo = false;
    float stateChangeTime = 0f;

    void Update()
    {
        if (turnOffParadigmCheck)
        {
            if (stateChangeTo == true)
                TurnOn();
            else
                TurnOff();
            return;
        }
            

        int score = 0;

        foreach(var b in binaryStates)
        {
            if (b.binary.State() == b.expectedValue)
                score++;
        }

        if (combineType == CombineType.AND)
            if (score == binaryStates.Count)
                TurnOn();
            else
                TurnOff();
        else if (score > 0)
            TurnOn();
        else
            TurnOff();
    }

    public override void TurnOn()
    {
       
        if (stateChangeTo != true)
        {
            stateChangeTime = Time.time + stateChangeDelay;
            stateChangeTo = true;
        }

        if (stateChangeTime > Time.time)
            return;

        if (state == true)
            return;

        state = true;
        foreach (var b in binariesAwaitingInput)
            b.TurnOn();
    }

    public override void TurnOff()
    {
        

        if (stateChangeTo != false)
        {
            stateChangeTime = Time.time + stateChangeDelay;
            stateChangeTo = false;
        }

        if (stateChangeTime > Time.time)
            return;

        if (state == false)
            return;

        state = false;
        foreach (var b in binariesAwaitingInput)
            b.TurnOff();
    }

    public override bool State()
    {
        return state;
    }

    public override void SwitchState()
    {
        return;
    }

    [System.Serializable]
    public class BinaryStateExpected {
        public ABinarySwitch binary;
        public bool expectedValue = true;
    }
}

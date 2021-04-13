using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimatedNumberTextHandler : MonoBehaviour
{
    TMP_Text text;
    public float totalAnimTime = 1f;
    public int value;
    public bool displayPlusWhenPositive;

    int currentDisplayedValue;
    float animEndTime;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        text.text = "";
    }

    private void OnEnable()
    {
        StartCoroutine("StartAnim");
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        text.text = "";
    }

    IEnumerator StartAnim()
    {
        animEndTime = Time.time + totalAnimTime;
        while(Time.time < animEndTime)
        {
            currentDisplayedValue = (int)Mathf.Lerp(0, value, (totalAnimTime-(animEndTime - Time.time))/totalAnimTime);
            text.text = GetStringToDisplay(currentDisplayedValue);
            yield return new WaitForEndOfFrame();
        }
        currentDisplayedValue = value;
        text.text = GetStringToDisplay(value);
    }

    string GetStringToDisplay(int value)
    {
        if (value > 0 && displayPlusWhenPositive)
            return "+" + value.ToString();
        else
            return value.ToString();
    }
}

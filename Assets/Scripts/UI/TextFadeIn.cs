using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFadeIn : MonoBehaviour
{
    TMP_Text text;
    public float totalAnimTime = 1f;
    float animStart;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        text.alpha = 0f;
    }

    private void OnEnable()
    {
        StartCoroutine("FadeIn");
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        text.alpha = 0f;
    }

    IEnumerator FadeIn()
    {
        animStart = Time.unscaledTime;
        while (Time.unscaledTime < animStart + totalAnimTime)
        {
            text.alpha = (Time.unscaledTime - animStart) / totalAnimTime;
            yield return new WaitForEndOfFrame();
        }
        text.alpha = 1f;
    }
}

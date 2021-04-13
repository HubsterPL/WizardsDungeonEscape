using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFlowIn : MonoBehaviour
{
    public Vector2 offset;
    TMP_Text text;
    public float totalAnimTime = 1f;
    float animStart;
    Vector4 originalMargins;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        originalMargins = text.margin;
        text.margin = offset;
    }

    private void OnEnable()
    {
        StartCoroutine("FadeIn");
    }

    private void OnDisable()
    {
        StopAllCoroutines();

    }

    IEnumerator FadeIn()
    {
        animStart = Time.unscaledTime;
        while (Time.unscaledTime < animStart + totalAnimTime)
        {
            text.margin = Vector4.Lerp(OffsetToMargins, originalMargins, (Time.unscaledTime - animStart) / totalAnimTime);
            yield return new WaitForEndOfFrame();
        }
        text.margin = originalMargins;

    }

    Vector4 OffsetToMargins {
        get {
            return originalMargins+ new Vector4(-offset.x, -offset.y, offset.x, offset.y);
        }    
    }
    
}

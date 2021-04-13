using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTypeIn : MonoBehaviour
{
    string record = "";
    TMP_Text text;
    public float totalAnimTime = 1f;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        record = text.text;
        text.text = "";
    }

    private void OnEnable()
    {
        StartCoroutine("TypeIn");
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        text.text = "";
    }

    IEnumerator TypeIn()
    {
        foreach(var c in record)
        {
            text.text += c;
            yield return new WaitForSeconds(totalAnimTime/record.Length);
        }
    }
}

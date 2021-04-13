using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisplayText : MonoBehaviour
{
    TMPro.TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.pause)
            UpdateText();
    }

    void UpdateText()
    {
        string str = "";

        int imm = Mathf.FloorToInt(Game.Score.GetTimeValue() / 60);
        if (imm < 10)
            str += "0";
        str += imm.ToString() + ":";

        int iss = Mathf.FloorToInt(Game.Score.GetTimeValue() % 60);
        if (iss < 10)
            str += "0";
        str += iss.ToString();

        text.text =  str;
    }
}

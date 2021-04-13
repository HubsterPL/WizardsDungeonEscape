using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTextController : MonoBehaviour
{
    TMP_Text textComp;
    int currentDisplay = 0;

    private void Start()
    {
        textComp = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        int score = Game.Score.GetScore();
        int toAdd = score - currentDisplay;
        toAdd = toAdd / 10;
        currentDisplay += toAdd;
        if (currentDisplay < score)
            currentDisplay++;
        else if (currentDisplay > score)
            currentDisplay--;
        textComp.text = currentDisplay.ToString();
    }
}

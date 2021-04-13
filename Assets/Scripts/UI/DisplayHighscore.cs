using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscore : MonoBehaviour
{
    public LevelData level;
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        text.text = GameManager.Instance.GetHighscore(level).ToString();
    }
}

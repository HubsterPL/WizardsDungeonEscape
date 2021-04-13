using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaBar : MonoBehaviour
{
    [SerializeField] Image manaFill;
    [SerializeField] Image overflowFill;
    [SerializeField] TMPro.TMP_Text valueDispaly;


    void Update()
    {
        float value = (float)Game.Score.GetMana() / (float)Game.Score.MaxMana;
        manaFill.fillAmount = value;
        overflowFill.fillAmount = value - 1f;
        valueDispaly.text = Game.Score.GetMana().ToString();
    }
}

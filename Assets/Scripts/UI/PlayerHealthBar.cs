using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthBar : MonoBehaviour
{
    DamageController player;
    [SerializeField] Image image;
    [SerializeField] TMPro.TMP_Text valueDispaly;


    void Update()
    {
        if (player != null)
        {
            float value = (float)player.Health / player.MaxHealth;
            image.fillAmount = value;
            valueDispaly.text = player.Health.ToString();
        }
    }

    public void SetSource(DamageController damageController)
    {
        player = damageController;
    }
}

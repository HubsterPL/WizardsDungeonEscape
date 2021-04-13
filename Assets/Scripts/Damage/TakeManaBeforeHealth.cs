using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(DamageController))]

public class TakeManaBeforeHealth : MonoBehaviour
{
    DamageController target;
    public float factor = .75f;
    void Start()
    {
        target = GetComponent<DamageController>();
        target.onDamageTaken.AddListener(new UnityAction<int, float>(RecoverHealthWithMana));
    }

    void RecoverHealthWithMana(int damage, float immunityDuration)
    {
        damage = Mathf.FloorToInt(damage * factor);
        if (Game.Score.GetMana() > damage)
        {
            target.UndoDamage(damage);
            Game.Score.AddMana(-damage);
        } else
        {
            target.UndoDamage(Game.Score.GetMana());
            Game.Score.AddMana(-Game.Score.GetMana());
        }
    }
}

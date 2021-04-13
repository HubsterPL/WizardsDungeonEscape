using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] int damage = 5;
    [SerializeField] float immunityFrames = .2f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var damageController = collision.GetComponent<DamageController>();
        if(damageController != null)
            damageController.DealDamage(damage, immunityFrames);
    }
}

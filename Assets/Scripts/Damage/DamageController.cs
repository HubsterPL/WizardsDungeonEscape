using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntFloatEvent : UnityEvent<int, float> { }

public class DamageController : MonoBehaviour
{
    [SerializeField] SpriteRenderer renderer;
    public UnityEvent onDeath = new UnityEvent();
    public UnityEvent<int, float> onDamageTaken = new IntFloatEvent();

    [SerializeField] int health = 100;
    [SerializeField] int maxHealth = 100;
    [SerializeField] bool immunityFrame = false;

    public bool ImmunityFrame { get { return immunityFrame; } }

    public void DealDamage(int amount, float immunityTime)
    {
        if (immunityFrame)
            return;

        immunityFrame = true;
        StartCoroutine("ImmunityFrames", immunityTime);

        if (onDamageTaken != null)
            onDamageTaken.Invoke(amount, immunityTime);

        health -= amount;
        if (health <= 0)
            Kill();
    }

    public void UndoDamage(int amount)
    {
        health += amount;
    }

    public void Kill()
    {
        StopAllCoroutines();
        renderer.material.SetFloat("_HighlightValue", 0f);
        health = 0;
        if(onDeath != null)
            onDeath.Invoke();
        
    }

    IEnumerator ImmunityFrames(float duration)
    {
        renderer.material.SetFloat("_HighlightValue", .4f);
        yield return new WaitForSeconds(duration);
        renderer.material.SetFloat("_HighlightValue", 0f);
        immunityFrame = false;
    }

    public int Health { get { return health; } }
    public int MaxHealth { get { return maxHealth; } }
}

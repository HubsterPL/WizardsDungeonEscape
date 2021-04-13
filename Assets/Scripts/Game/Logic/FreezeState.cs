using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeState : MonoBehaviour
{
    // References
    Rigidbody2D rb2d;
    RigidbodyConstraints2D constraintsToRestore;
    SpriteRenderer renderer;
    StatusWorldCanvas status;

    // Global private variables
    const float manaDecayRate = 1f; // 2 == 2 per sec
    int mana;
    float manaDecay;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        manaDecay = 0f;
        rb2d = GetComponent<Rigidbody2D>();
        constraintsToRestore = rb2d.constraints;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        status = Instantiate<StatusWorldCanvas>(GameManager.Instance.Settings.freezeStatusBarPrefab);
        status.SetFollowTarget(transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        renderer.material.SetFloat("_OutlineSize", 1f);
        renderer.material.SetColor("_OutlineColor", Color.Lerp(new Color(0f, 0f, 0f, 1f), GameManager.Instance.Settings.outlineFreeze, (float)mana / 10));
        manaDecay += Time.fixedDeltaTime * manaDecayRate;

        if(manaDecay >= 1f)
        {
            mana -= Mathf.FloorToInt(manaDecay);
            manaDecay -= Mathf.FloorToInt(manaDecay);
            UpdateStatusBar();
        }

        if (mana <= 0)
        {
            mana = 0;
            Unfreeze();
        }
    }

    private void UpdateStatusBar()
    {
        if(status != null)
            status.SetValues(mana, (float)mana / 10f);
    }

    public void Unfreeze()
    {
        Game.Score.AddMana(GetMana()/2);
        Destroy(status.gameObject);
        Destroy(this);
    }

    public void AddManaCharge(int amount)
    {
        mana += amount;
        UpdateStatusBar();
    }

    public int GetMana()
    {
        return mana;
    }

    private void OnDestroy()
    {
        rb2d.constraints = constraintsToRestore;
        renderer.material.SetFloat("_OutlineSize", 0f);
    }
}

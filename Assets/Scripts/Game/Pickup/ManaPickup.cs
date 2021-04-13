using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : MonoBehaviour
{
    public int reward = 20;
    public GameObject particleEffect;

    private void Awake()
    {
        GameManager.Instance.PickupCountOnLevel++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (particleEffect != null)
                Instantiate(particleEffect).transform.position = transform.position;

            GameAudioManager.Instance.PlayPotionPickup();
            GameManager.Instance.PickupCount++;

            Game.Score.AddPickup(reward);
            Game.Score.AddMana(reward);
            Destroy(gameObject);
        }
    }
}

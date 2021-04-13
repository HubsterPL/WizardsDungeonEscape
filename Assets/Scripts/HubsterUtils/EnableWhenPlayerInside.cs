using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWhenPlayerInside : MonoBehaviour
{
    public GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target.SetActive(false);
        }
    }
}

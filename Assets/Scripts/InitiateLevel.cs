using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateLevel : MonoBehaviour
{
    public int startScore;
    // Start is called before the first frame update
    void Start()
    {
        Game.Score.SetMana(startScore);
    }

}

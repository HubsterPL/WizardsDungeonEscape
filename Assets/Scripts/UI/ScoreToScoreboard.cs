using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreToScoreboard : MonoBehaviour
{
    [SerializeField]AnimatedNumberTextHandler animatedNumber;
    public enum ScoreType
    {
        MANA, TIME, PICKUP, ENEMY, SUM
    }
    public ScoreType scoreType = ScoreType.MANA;

    public void OnEnable()
    {
        if (scoreType == ScoreType.MANA)
            FetchMana();
        else if (scoreType == ScoreType.PICKUP)
            FetchPickups();
        else if (scoreType == ScoreType.TIME)
            FetchTime();
        else if (scoreType == ScoreType.ENEMY)
            FetchEnemies();
        else if (scoreType == ScoreType.SUM)
            FetchSum();
    }

    public void FetchMana()
    {
        animatedNumber.value = Game.Score.GetMana();
    }

    public void FetchTime()
    {
        animatedNumber.value = Game.Score.GetTimeScore();
    }

    public void FetchPickups() 
    {
        animatedNumber.value = Game.Score.GetPickup();
    }

    public void FetchEnemies()
    {
        animatedNumber.value = Game.Score.GetEnemies();
    }

    public void FetchSum()
    {
        animatedNumber.value = Game.Score.GetTimeScore() + Game.Score.GetPickup() + Game.Score.GetEnemies();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Level Data", menuName = "Game/Level")]
public class LevelData : ScriptableObject
{
    public string levelSceneAssetName;
    public string levelName;
    public int manaLimit;
    public int timeReward;
}

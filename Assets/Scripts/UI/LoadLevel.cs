using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public LevelData levelData;

    public void Load()
    {
        GameManager.Instance.LoadLevel(levelData);
    }
}

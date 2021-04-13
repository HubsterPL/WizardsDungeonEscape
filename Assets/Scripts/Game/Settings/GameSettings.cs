using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Game/Game Settings")]
public class GameSettings : ScriptableObject
{
    [ColorUsage(true, true)]
    public Color outlineProximityInteractive;
    [ColorUsage(true, true)]
    public Color outlineProximityLocked;
    [ColorUsage(true, true)]
    public Color outlineTelekinesis;
    [ColorUsage(true, true)]
    public Color outlineTelekinesisActive;
    [ColorUsage(true, true)]
    public Color outlineFreeze;

    public List<LevelData> levels = new List<LevelData>();

    public StatusWorldCanvas freezeStatusBarPrefab;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPrefsContext : MonoBehaviour
{
    [ContextMenu("Reset PlayerPrefs")]
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCountText : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text text;

    private void OnEnable()
    {
        text.text = GameManager.Instance.EnemyCount + "/" + GameManager.Instance.EnemyCountOnLevel;
    }

}

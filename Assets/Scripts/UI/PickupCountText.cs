using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCountText : MonoBehaviour
{

    [SerializeField]TMPro.TMP_Text text;

    private void OnEnable()
    {
        text.text = GameManager.Instance.PickupCount + "/" + GameManager.Instance.PickupCountOnLevel;
    }

}

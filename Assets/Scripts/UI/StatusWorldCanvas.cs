using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusWorldCanvas : MonoBehaviour
{
    public Text text;
    public Image fillImage;
    Transform followTarget;

    private void LateUpdate()
    {
        if(followTarget != null)
            Follow();
    }
    void Follow()
    {
        transform.position = new Vector3(followTarget.position.x, followTarget.position.y + .5f, 0f);
    }

    public void SetFollowTarget(Transform target)
    {
        followTarget = target;
    }

    public void SetValues(int value, float fillAmount)
    {
        text.text = value.ToString();
        fillImage.fillAmount = fillAmount;
    }
}

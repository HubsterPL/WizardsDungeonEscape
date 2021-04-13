using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.right = (Vector3)(Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }
}

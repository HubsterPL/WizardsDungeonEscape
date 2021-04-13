using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteWidthToScaleX : MonoBehaviour
{
    public SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(renderer.size.x, transform.localScale.y, transform.localScale.z);
    }
}

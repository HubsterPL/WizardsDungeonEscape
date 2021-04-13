using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rail2D : MonoBehaviour
{
    public SpriteRenderer renderer;
    [SerializeField]float length = 10f;

    public float Length {
        get { return length; }
        set {
			length = value;
            UpdateRendererSize();
        }
    }

    public Rail2D topNext;
    public Rail2D bottomNext;

    private void Start()
    {
        UpdateRendererSize();
    }

    private void UpdateRendererSize()
    {
        if (renderer != null)
            renderer.size = new Vector2(renderer.size.x, length);
    }

    public Vector2 GetTopPos()
    {
        return transform.position + transform.up * length / 2f;
    }

    public Vector2 GetBottomPos()
    {
        return transform.position - transform.up * length / 2f;
    }
}

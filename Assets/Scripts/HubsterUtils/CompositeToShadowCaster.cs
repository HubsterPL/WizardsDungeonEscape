using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(CompositeCollider2D))]
public class CompositeToShadowCaster : MonoBehaviour
{
    
    CompositeCollider2D compositeCollider;
    List<ShadowCaster2D> shadowCasterPool = new List<ShadowCaster2D>();

    void Start()
    {
        compositeCollider = GetComponent<CompositeCollider2D>();
        ShapeToComposite();
    }

    void ShapeToComposite()
    {
        FieldInfo shapeField = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);
        FieldInfo hashField = typeof(ShadowCaster2D).GetField("m_ShapePathHash", BindingFlags.NonPublic | BindingFlags.Instance);

        List<Vector2> path = new List<Vector2>();

        for(int pathId = 0; pathId < compositeCollider.pathCount; pathId++)
        {
            compositeCollider.GetPath(pathId, path);
            if(shadowCasterPool.Count <= pathId)
            {
                shadowCasterPool.Add(new GameObject("ShadowCaster").AddComponent<ShadowCaster2D>());
                shadowCasterPool[pathId].transform.SetParent(transform);
                shadowCasterPool[pathId].selfShadows = true;
            }
            shapeField.SetValue(shadowCasterPool[pathId], V2ListToV3Table(path));
            hashField.SetValue(shadowCasterPool[pathId], Random.Range(int.MinValue, int.MaxValue));
        }
    }

    Vector3[] V2ListToV3Table(List<Vector2> lv2)
    {
        Vector3[] l = new Vector3[lv2.Count];
        for (int i = 0; i < lv2.Count; i++)
            l[i] = (lv2[i]);
        return l;
    }

}

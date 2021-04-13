using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PotionRenderer : MonoBehaviour
{
    [SerializeField] int potionShaderHash;
    static Dictionary<int, Material> materials;

    [ColorUsage(showAlpha: false, hdr: true)]    
    [SerializeField] Color color = Color.blue;
    [SerializeField] Sprite main;
    [SerializeField] Sprite liquidMask;

    private void Start()
    {
        UpdateMaterial();
    }

    private void OnValidate()
    {
        if(gameObject.activeInHierarchy)
            UpdateMaterial();
    }

    void UpdateMaterial()
    {
        /*
        if(materials == null)
        {
            materials = new Dictionary<int, Material>();
        }

        Material material;
        if (!materials.TryGetValue(potionShaderHash, out material))
        {
            Shader shader = Shader.Find("Shader Graphs/PickupPotion");
            material = new Material(shader);
            materials.Add(potionShaderHash, material);
        }
        else if(material == null)
        {
            Shader shader = Shader.Find("Shader Graphs/PickupPotion");
            material = new Material(shader);
            materials[potionShaderHash] = material;
        }*/

        Shader shader = Shader.Find("Shader Graphs/PickupPotion");
        Material material = new Material(shader);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = main;
        material.SetColor("_LiqColor", color);
        material.SetTexture("_EmTex", liquidMask.texture);
        renderer.material = material;
    }

}

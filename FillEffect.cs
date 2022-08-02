using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class FillEffect : MonoBehaviour
{
    readonly int paintColorId = Shader.PropertyToID("_PaintColor");
    readonly int colorId = Shader.PropertyToID("_Color");
    readonly int currentTimeId = Shader.PropertyToID("_CurrentTime");
    readonly int durationId = Shader.PropertyToID("_Duration");
    readonly int centerId = Shader.PropertyToID("_Center");

    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Material material;
    [SerializeField]
    public Color color = Color.white;
    [SerializeField]
    float duration = 2f;

    Material spriteMaterial => spriteRenderer.material;

    void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material = material;
    }

    void Awake()
    {
        spriteMaterial.SetColor(paintColorId, color);
        spriteMaterial.SetColor(colorId, color);
    }

    public void FillColor(Color color, Vector2 uvPosition) 
    {
        this.color = color;
        spriteMaterial.SetVector(centerId, uvPosition);
        spriteMaterial.SetFloat(currentTimeId, Time.time);
        spriteMaterial.SetColor(paintColorId, color);
        spriteMaterial.SetFloat(durationId, duration);
        StopAllCoroutines();
        StartCoroutine(SetBaseColor(color));
    }
    public void Clear()
    {
        StopAllCoroutines();
        color = Color.white;
        spriteMaterial.SetColor(paintColorId, color);
        spriteMaterial.SetColor(colorId, color);
    }

    IEnumerator SetBaseColor(Color color)
    {
        yield return new WaitForSeconds(duration);
        spriteMaterial.SetColor(paintColorId, color);
        spriteMaterial.SetColor(colorId, color);
    }
}

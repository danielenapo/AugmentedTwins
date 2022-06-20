//took from here: https://stackoverflow.com/questions/72245314/how-to-fade-out-in-a-canvas-canvas-group-and-its-children-objects
using System.Collections.Generic;
using UnityEngine;
public class AlphaGroup : MonoBehaviour
{
    [Range(0, 1)]
    public float alpha = 1f;

    public float Alpha
    {
        get => alpha;
        set
        {
            alpha = value;

            UIGroup.alpha = alpha; // setup CanvasGroup Alpha

            SetMaterialsAlpha(materials);

            foreach (var _meshRenderer in MeshRenderers) // for all mesh renderer materials
            {
                if (!_meshRenderer) continue;
                SetMaterialsAlpha(_meshRenderer.materials);
            }

            foreach (var _sprite in Sprites) // setup sprites
            {
                if (!_sprite) continue;
                var col = _sprite.color;
                col.a = alpha;
                _sprite.color = col;
            }
        }
    }
    private void SetMaterialsAlpha(IEnumerable<Material> _materials)
    {
        foreach (var material in _materials)
        {
            if (!material) continue;
            var col = material.color;
            col.a = alpha;
            material.color = col;
        }
    }
    public void OnValidate() => Alpha = alpha;

    [Header("Your Canvas Group")]
    public CanvasGroup UIGroup;

    [Header("Your Sprites")]
    public SpriteRenderer[] Sprites;

    [Header("Your Mesh Renderers")]
    public MeshRenderer[] MeshRenderers;

    [Header("Your materials")]
    public Material[] materials;
}
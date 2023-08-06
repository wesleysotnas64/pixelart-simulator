using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcess : MonoBehaviour
{
    [Header("Pixelization")]
    public bool activePixelization;
    public Material pixelMaterial;
    [Range(1, 512)]
    public int pixelSize;

    [ExecuteInEditMode]
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (activePixelization)
        {
            pixelMaterial.SetInt("_PixelSize", pixelSize);
            Graphics.Blit(source, destination, pixelMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}

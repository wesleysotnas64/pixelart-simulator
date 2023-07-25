using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public List<MeshRenderer> materialList;

    [Range(0, 1)]
    public float ambientStrength;

    [Range(0, 1)]
    public float ambientStrengthReflectivity;

    [Range(0, 1)]
    public float lightStrength;

    [Range(0, 1)]
    public float difuseStrengthReflectivity;

    [Range(0, 1)]
    public float specularStrengthReflectivity;

    [Range(1, 128)]
    public int shininess;

    private void Start()
    {
        ambientStrength = 0.1f;
        ambientStrengthReflectivity = 0.1f;
        lightStrength = 1.0f;
        difuseStrengthReflectivity = 0.5f;
        specularStrengthReflectivity = 0.8f;
        shininess = 32;
    }

    void Update()
    {
        SetMaterialsParameters();
    }

    private void SetMaterialsParameters()
    {
        foreach(MeshRenderer material in materialList)
        {
            material.material.SetFloat("_AmbientStrength", ambientStrength);
            material.material.SetFloat("_AmbientStrengthReflectivity", ambientStrengthReflectivity);
            material.material.SetFloat("_LightStrength", lightStrength);
            material.material.SetFloat("_DifuseStrengthReflectivity", difuseStrengthReflectivity);
            material.material.SetFloat("_SpecularStrengthReflectivity", specularStrengthReflectivity);
            try
            {
                material.material.SetInt("_Shininess", shininess);
            }
            catch{}
        }
    }
}

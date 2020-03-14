using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Fx : MonoBehaviour
{
    public Shader FXshader;
    private Material FXMaterial;
    // Use this for initialization
    void CreateMaterials()
    {
        if (FXMaterial == null)
        {
            FXMaterial = new Material(FXshader);
            FXMaterial.hideFlags = HideFlags.HideAndDontSave;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        CreateMaterials();
        Graphics.Blit(source, destination, FXMaterial);
    }
}

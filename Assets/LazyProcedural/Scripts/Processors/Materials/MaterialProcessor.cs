using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialProcessor
{
    public static Material defaultMaterial;
    public Material Process(Material sourceMaterial)
    {
        if (sourceMaterial != null) return sourceMaterial;

        if (defaultMaterial == null)
            AssignDefaultMaterial();

        return defaultMaterial;
    }

    private static void AssignDefaultMaterial()
    {
        defaultMaterial = CreateDefaultMaterial();
    }

    public static Material CreateDefaultMaterial()
    {
        if (UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset == null)
            return new Material(Shader.Find("Standard"));
        else
            return new Material(UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset.defaultMaterial);
    }

    public static Material CreateDefaultLineMaterial()
    {
        if (UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset == null)
            return new Material(Shader.Find("Standard Unlit"));
        else
            return new Material(UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset.defaultUIOverdrawMaterial);
    }
}

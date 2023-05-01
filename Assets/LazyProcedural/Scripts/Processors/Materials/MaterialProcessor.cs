using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialProcessor
{
   public Material Process(Sceelix.Actors.Data.Material sourceMaterial)
    {
        Material material = CreateDefaultMaterial();

        return material;
        //material.color=sourceMaterial.
    }

    public static Material CreateDefaultMaterial()
    {
        if (UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset == null)
            return new Material(Shader.Find("Standard"));
        else
            return new Material(UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset.defaultMaterial);
    }
}

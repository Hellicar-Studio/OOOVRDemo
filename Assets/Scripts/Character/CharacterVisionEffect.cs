using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisionEffect : MonoBehaviour {

    protected Material material;
    public string shaderName;
    // Use this for initialization
    protected void Awake()
    {
        material = new Material(Shader.Find(shaderName));
    }

    // Postprocess the image
    protected void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        setUniforms();
        Graphics.Blit(source, destination, material);
    }

    protected virtual void setUniforms()
    {

    }
}

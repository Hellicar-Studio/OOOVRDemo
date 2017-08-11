using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisionEffect : MonoBehaviour {

    protected Material material;
    public Shader shader;

    protected void Awake()
    {
        material = new Material(shader);
    }

    protected void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        setUniforms();
        Graphics.Blit(source, destination, material);
    }

    protected virtual void setUniforms()
    {

    }
}

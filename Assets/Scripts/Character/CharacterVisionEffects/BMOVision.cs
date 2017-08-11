using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BMOVision : MonoBehaviour
{

    //public float intensity;
    public float deltaX, deltaY;
    private Material material;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Hidden/BMOVision"));
    }

    public void setShader(Shader s)
    {
        material = new Material(s);
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //if (intensity == 0)
        //{
        //    Graphics.Blit(source, destination);
        //    return;
        //}

        //material.SetFloat("_bwBlend", );
        material.SetFloat("_DeltaX", deltaX);
        material.SetFloat("_DeltaY", deltaY);

        Graphics.Blit(source, destination, material);
    }
}
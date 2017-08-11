using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BMOVision : CharacterVisionEffect
{
    //public float intensity;
    public float deltaX, deltaY;

    protected override void setUniforms()
    {
        material.SetFloat("_DeltaX", deltaX);
        material.SetFloat("_DeltaY", deltaY);
    }
}
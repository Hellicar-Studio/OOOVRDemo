using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisionEffect : MonoBehaviour {

	[HideInInspector]
    public Material material;

    public Shader shader;

    protected void Awake()
    {
        material = new Material(shader);
    }

    public virtual void setUniforms()
    {

    }
}

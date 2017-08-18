using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorSetter : MonoBehaviour {

	public Material material;

	public string uniform;

	public Color color;

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetColor(uniform, color);
	}
}

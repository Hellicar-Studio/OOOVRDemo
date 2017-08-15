using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBGVision : CharacterVisionEffect {

	public float vignettePower;
	public Color tint;

	public override void setUniforms()
	{
		material.SetColor("_Tint", tint);
		material.SetFloat("_VignettePower", vignettePower);
	}
}

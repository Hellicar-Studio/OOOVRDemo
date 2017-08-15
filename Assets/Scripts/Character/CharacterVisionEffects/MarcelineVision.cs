﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcelineVision : CharacterVisionEffect {

	public float intensity;
	public float contrast;
 //   public Color R = new Color(0.57f, 0.43f, 0.0f);
	//public Color G = new Color(0.33f, 0.66f, 0.0f);
	//public Color B = new Color(0.0f, 0.24f, 0.76f);

    public override void setUniforms()
    {
		material.SetFloat("_bwBlend", intensity);
		material.SetFloat("_contrast", contrast);
        //material.SetColor("_R", R);
        //material.SetColor("_G", G);
        //material.SetColor("_B", B);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
	public AudioClip[] soundClips;

	public AudioClip getRandomClip()
	{
		return soundClips[Random.Range(0, soundClips.Length)];
	}
}

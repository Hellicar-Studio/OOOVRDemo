using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
	public AudioSource characterAudioSource;
	public AudioClip[] characterSounds;

	private AudioClip GetRandomCharacterSound()
	{
		return characterSounds[Random.Range(0, characterSounds.Length - 1)];
	}

	public void Play()
	{
		if (characterAudioSource.isPlaying)
			return;

		characterAudioSource.clip = GetRandomCharacterSound();
		characterAudioSource.Play();
	}
}

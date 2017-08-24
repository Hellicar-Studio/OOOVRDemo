using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
	public AudioSource characterAudioSource;
	[HideInInspector]
	public AudioClip[] defaultSounds;
	public AudioClip[] sounds;
	public CharacterManager[] characters;
	Dictionary<CharacterManager, AudioClip> characterSounds;

	void Awake()
	{
		characterSounds = new Dictionary<CharacterManager, AudioClip>();

		if(sounds.Length == characters.Length)
		{
			for(int i = 0; i < sounds.Length; i++)
			{
				characterSounds.Add(characters[i], sounds[i]);
			}
		}
		else
		{
			Debug.Log("Characters and Sounds do not match!");
		}
	}
	private AudioClip GetRandomCharacterSound()
	{
		return defaultSounds[Random.Range(0, defaultSounds.Length)];
	}

	private AudioClip GetCharacterSpecificSound(CharacterManager currentManager)
	{
		try
		{
			return characterSounds[currentManager];
		}
		catch
		{
			return GetRandomCharacterSound();
		}
	}

	public void Play()
	{
		if (characterAudioSource.isPlaying)
			return;

		characterAudioSource.clip = GetRandomCharacterSound();
		characterAudioSource.Play();
	}

	public void Play(CharacterManager currentCharacter)
	{
		if (characterAudioSource.isPlaying)
			return;

		characterAudioSource.clip = GetCharacterSpecificSound(currentCharacter);
		characterAudioSource.Play();
	}
}

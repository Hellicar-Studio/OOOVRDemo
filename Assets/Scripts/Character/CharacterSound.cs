using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
	public AudioSource characterAudioSource;
	public SoundLibrary defaultSoundLibrary;
	public SoundLibrary[] soundLibraries;
	public CharacterManager[] characters;
	Dictionary<CharacterManager, SoundLibrary> characterSounds;

	void Awake()
	{
		characterSounds = new Dictionary<CharacterManager, SoundLibrary>();

		if(soundLibraries.Length == characters.Length)
		{
			for(int i = 0; i < soundLibraries.Length; i++)
			{
				characterSounds.Add(characters[i], soundLibraries[i]);
			}
		}
		else
		{
			Debug.Log("Characters and soundLibraries do not match!");
		}
	}

	private AudioClip GetCharacterSpecificSound(CharacterManager currentManager)
	{
		try
		{
			return characterSounds[currentManager].getRandomClip();
		}
		catch
		{
			return defaultSoundLibrary.getRandomClip();
		}
	}

	public void Play()
	{
		if (characterAudioSource.isPlaying)
			return;

		characterAudioSource.clip = defaultSoundLibrary.getRandomClip();
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

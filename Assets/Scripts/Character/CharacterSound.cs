using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
	public AudioSource characterAudioSource;
	private SoundLibrary defaultSoundLibrary;
	private Dictionary<CharacterManager, SoundLibrary> characterSounds;

	void Awake()
	{
		characterSounds = new Dictionary<CharacterManager, SoundLibrary>();

		SoundLibrary[] soundLibraries = GetComponents<SoundLibrary>();

		for(int i = 0; i < soundLibraries.Length; i++)
		{
			if(soundLibraries[i].characterKey == null)
			{
				defaultSoundLibrary = soundLibraries[i];
			}
			else
			{
				characterSounds.Add(soundLibraries[i].characterKey, soundLibraries[i]);
			}
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

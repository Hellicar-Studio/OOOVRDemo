using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour 
{
	public MicrophoneManager microphoneManager;
	public int minimumLevel = 50;
	public ParticleSystem particles;

	public void TriggerVoiceEffect()
	{
		if ((int)microphoneManager.GetMicrophoneInputLevel() <= minimumLevel)
			ToggleParticles(false);
		else
			ToggleParticles(true);
	}

	private void ToggleParticles(bool isPlaying)
	{
		if (isPlaying)
			particles.Play();
		else
			particles.Stop();
	}
}

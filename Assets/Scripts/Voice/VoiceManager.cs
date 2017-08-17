using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour 
{
	public MicrophoneManager microphoneManager;
	public int minimumLevel = 50;
	public ParticleSystem particles;

	private float[] micHistory;
	public int historyLength = 120;
	public bool pulse = false;

	private void Start()
	{
		micHistory = new float[historyLength];
	}

	void Update()
	{
		float MicLoudness = microphoneManager.GetMicrophoneInputLevel();
		float avgMicLoudness = 0;
		for (int i = historyLength - 1; i > 0; i--)
		{
			micHistory[i] = micHistory[i - 1];
			avgMicLoudness += micHistory[i];
		}
		micHistory[0] = MicLoudness;
		avgMicLoudness /= historyLength;
		if (MicLoudness > avgMicLoudness * 2 + minimumLevel)
		{
			pulse = true;
		}
		else
		{
			pulse = false;
		}
	}

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

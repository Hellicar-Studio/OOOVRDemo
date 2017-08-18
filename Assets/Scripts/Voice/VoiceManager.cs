using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour 
{
	public MicrophoneManager microphoneManager;
	public int minimumLevel = 50;
	public ParticleSystem particles;
	public VoiceAnimation voiceAnimation;
	public int historyLength = 120;
	public bool pulse = false;
	public float soundCooldownDuration;

	private float[] micHistory;
	private bool isReadyToReactToSound;

	private void Start()
	{
		micHistory = new float[historyLength];
		isReadyToReactToSound = true;
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
		if (MicLoudness > avgMicLoudness * 2 + minimumLevel && isReadyToReactToSound)
		{
			pulse = true;
			voiceAnimation.PulseVoice();
			StartCoroutine(SoundCooldown());
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

	public IEnumerator SoundCooldown()
	{
		isReadyToReactToSound = false;
		float timeCalled = Time.time;
		while (Time.time - timeCalled < soundCooldownDuration)
		{
			yield return null;
		}
		isReadyToReactToSound = true;
	}
}

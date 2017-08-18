using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
	public CharacterAnimation characterAnimation;
	public CharacterSound  characterSound;
	public CharacterEffect characterEffect;
	public CharacterMeta characterMeta;
	public CharacterVisionEffect characterVisionEffect;
	public CharacterMover characterMover;

	public bool isSoundReactive;

	public float soundCooldownDuration = 0.5f;

	[HideInInspector]
	public bool isReadyToReactToSound;

	public void Start()
	{
		isReadyToReactToSound = true;
	}

	public CharacterMeta GetCharacterMeta()
	{
		return characterMeta;
	}

	public void PlayAnimation(string animationStateName)
	{
		characterAnimation.TriggerAnimationState(animationStateName);
	}

	public void PlaySound()
	{
		characterSound.Play();
	}

	public void PlayEffect()
	{
		characterEffect.SpawnParticles();
	}

	public void EnableVisionEffect()
	{
		if (characterVisionEffect != null)
			characterVisionEffect.enabled = true;
	}

	public void DisableVisionEffect()
    {
		if (characterVisionEffect != null)
			characterVisionEffect.enabled = false;
	}

	public void EnableMoverTriggerState()
	{
		if(characterMover != null)
			characterMover.enableTriggerState();
	}

	public void EnableMoverIdleState()
	{
		if (characterMover != null)
			characterMover.enableIdleState();
	}

	public IEnumerator SoundCooldown()
	{
		isReadyToReactToSound = false;
		float timeCalled = Time.time;
		while(Time.time - timeCalled < soundCooldownDuration)
		{
			yield return null;
		}
		isReadyToReactToSound = true;
	}
}

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

	private bool hasSoundPlayed;

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

    public void ApplyVisionEffect()
    {
        if(characterVisionEffect != null)
            characterVisionEffect.enabled = true;
    }
    public void RemoveVisionEffect()
    {
        if (characterVisionEffect != null)
            characterVisionEffect.enabled = false;
    }
}

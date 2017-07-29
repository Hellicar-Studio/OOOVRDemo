using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
	public Animator characterAnimator;

	public bool AnimatorIsPlaying()
	{
		return characterAnimator.GetCurrentAnimatorStateInfo(0).length > characterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	public bool AnimatorIsPlaying(string animationStateName)
	{
		return AnimatorIsPlaying() && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationStateName);
	}

	public void TriggerAnimationState(string animationStateName)
	{
		if (AnimatorIsPlaying(animationStateName))
		{
			characterAnimator.ResetTrigger(animationStateName);
			return;
		}
		
		characterAnimator.SetTrigger(animationStateName);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController: MonoBehaviour
{
	public Animator animation;

	public bool AnimatorIsPlaying()
	{
		return animation.GetCurrentAnimatorStateInfo(0).length > animation.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	public bool AnimatorIsPlaying(string stateName)
	{
		return AnimatorIsPlaying() && animation.GetCurrentAnimatorStateInfo(0).IsName(stateName);
	}

	public AnimatorStateInfo GetCurrentState()
	{
		return animation.GetCurrentAnimatorStateInfo(0);
	}
}

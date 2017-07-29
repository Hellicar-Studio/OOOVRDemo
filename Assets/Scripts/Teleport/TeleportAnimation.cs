using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAnimation : MonoBehaviour
{
	public Animator teleportAnimation;

	public bool AnimatorIsPlaying()
	{
		return teleportAnimation.GetCurrentAnimatorStateInfo(0).length > teleportAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	public bool AnimatorIsPlaying(string stateName)
	{
		return AnimatorIsPlaying() && teleportAnimation.GetCurrentAnimatorStateInfo(0).IsName(stateName);
	}

	public AnimatorStateInfo GetCurrentState()
	{
		return teleportAnimation.GetCurrentAnimatorStateInfo(0);
	}

	public IEnumerator CloseEye(float duration)
	{
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			teleportAnimation.Play("CloseEye", 0, t / duration);
			yield return null;
		}
	}

	public IEnumerator OpenEye(float duration)
	{
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			teleportAnimation.Play("OpenEye", 0, t / duration);
			yield return null;
		}
	}
}

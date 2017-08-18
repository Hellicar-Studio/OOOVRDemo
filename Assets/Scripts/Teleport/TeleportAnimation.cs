using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAnimation : AnimationController
{
	public IEnumerator CloseEye(float duration)
	{
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			animation.Play("CloseEye", 0, t / duration);
			yield return null;
		}
	}

	public IEnumerator OpenEye(float duration)
	{
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			animation.Play("OpenEye", 0, t / duration);
			yield return null;
		}
	}
}

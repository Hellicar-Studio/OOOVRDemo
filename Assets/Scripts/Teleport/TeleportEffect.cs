using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffect : MonoBehaviour
{
	public Renderer[] teleportEffectRenderers;

	public IEnumerator ColorFadeToBlack(Color startCharacterColor, float duration)
	{
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			SetRendererColor(Color.Lerp(startCharacterColor, Color.black, t/duration));
			yield return null;
		}
	}

	public IEnumerator ColorFadeFromBlack(Color endCharacterColor, float duration)
	{
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			SetRendererColor(Color.Lerp(Color.black, endCharacterColor, t/duration));
			yield return null;
		}
	}

	private void SetRendererColor(Color currentCharacterColor)
	{
		foreach (Renderer effectRenderer in teleportEffectRenderers)
		{
			effectRenderer.material.SetColor("_Color", currentCharacterColor);
		}
	}
}

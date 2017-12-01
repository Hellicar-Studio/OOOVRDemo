using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorRenderer : MonoBehaviour 
{
	public Image playerCursor;
	public Color colorOpaque;
	public Color colorTransparent;
	public float duration = 2f;
	[HideInInspector]
	public bool isAlphaFull;

	private bool isAlphaFaded;

	void Start()
	{
		FadeToTransparent();
	}

	public void FadeToTransparent()
	{
		if (playerCursor.color != colorTransparent && !isAlphaFaded)
		{
			StartCoroutine(DecreaseAlpha());
			isAlphaFaded = true;
		}
	}

	public void FadeFromTransparent()
	{
		if (playerCursor.color != colorOpaque && isAlphaFaded)
		{
			StartCoroutine(IncreaseAlpha());
			isAlphaFaded = false;
		}
	}

	private IEnumerator DecreaseAlpha()
	{
		isAlphaFull = false;
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			playerCursor.color = Color.Lerp(colorOpaque, colorTransparent, t / duration);
			yield return null;
		}
	}

	private IEnumerator IncreaseAlpha()
	{
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			playerCursor.color = Color.Lerp(colorTransparent, colorOpaque, t / duration);
			yield return null;
		}
		isAlphaFull = true;
	} 
}

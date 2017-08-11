using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
	public Transform playerTransform;
	public TeleportAnimation teleportAnimation;
	public TeleportEffect teleportEffect;
	public TeleportCharacterRenderer teleportCharacterRenderer;
	public TeleportCharacterVisionEffect teleportCharacterVisionEffect;
	public Color startCharacterColor = Color.white;
	public float distancePadding = 0.5f;

	public void DetectTeleportTarget(CharacterManager characterManager, Transform teleportTransform)
	{
		if (teleportAnimation.AnimatorIsPlaying())
			return;

		StartCoroutine(ExecuteTeleportWithTransition(characterManager, teleportTransform));
	}

	private IEnumerator ExecuteTeleportWithTransition(CharacterManager characterManager, Transform teleportTransform)
	{
		AnimationBegin(characterManager, teleportTransform);

		while (true)
		{
			yield return new WaitForSeconds(0f);
			if (!teleportAnimation.AnimatorIsPlaying("CloseEye"))
			{
				AnimationEnd(characterManager, teleportTransform);
				yield break;
			}
		}
	}

	private void AnimationBegin(CharacterManager characterManager, Transform teleportTransform)
	{
		float duration = Random.Range(0.2f, 1.0f);
		StartCoroutine(teleportEffect.ColorFadeToBlack(startCharacterColor, duration));
		StartCoroutine(teleportAnimation.CloseEye(duration));
	}

	private void AnimationEnd(CharacterManager characterManager, Transform teleportTransform)
	{
		teleportCharacterVisionEffect.disableAllVisionEffects();
		teleportCharacterVisionEffect.enablevisionEffect(characterManager);
		float duration = Random.Range(1.0f, 3.0f);
		startCharacterColor = characterManager.characterMeta.characterColor;
		teleportCharacterRenderer.ToggleRenderers(characterManager.gameObject);
		TeleportPlayer(teleportTransform);
		StartCoroutine(teleportEffect.ColorFadeFromBlack(characterManager.characterMeta.characterColor, duration));
		StartCoroutine(teleportAnimation.OpenEye(duration));
	}

	private void TeleportPlayer(Transform teleportTransform)
	{
		playerTransform.position = teleportTransform.position + teleportTransform.forward * distancePadding;
		playerTransform.rotation = teleportTransform.rotation;
	}
}

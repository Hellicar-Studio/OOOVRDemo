using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
	public TeleportManager teleportManager;
	public VoiceManager voiceManager;
	public CursorRenderer cursorRenderer;
	public string actionAnimationState = "action";

	private bool canAnimationPlay = true;

	void Update()
	{
		DetectInteractiveObjects();
		ExecuteVoiceInteraction();
	}

	private void DetectInteractiveObjects()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit))
		{
			if (hit.collider.GetComponentInParent<CharacterManager>())
			{
				Debug.Log("Calling Handle Actions!");
				HandleActions(hit.collider);
				return;
			}
		}

		cursorRenderer.FadeToTransparent();
		canAnimationPlay = true;
	}

	private void HandleActions(Collider targetCollider)
	{
		CharacterManager characterManager = targetCollider.GetComponentInParent<CharacterManager>();
		if (!characterManager)
			return;

		ExecuteLookInteraction(characterManager);

		TeleportTarget teleportTarget = targetCollider.GetComponentInParent<TeleportTarget>();
		if (!teleportTarget)
			return;

		cursorRenderer.FadeFromTransparent();

		if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.One))
		{
			ExecuteTeleport(characterManager, teleportTarget.teleportTransform);
		}
	}

	private void ExecuteTeleport(CharacterManager characterManager, Transform teleportTransform)
	{
		teleportManager.DetectTeleportTarget(characterManager, teleportTransform);
		characterManager.PlaySound();
	}

	private void ExecuteLookInteraction(CharacterManager characterManager)
	{
		if (!canAnimationPlay)
			return;

		if (characterManager.isSoundReactive && !voiceManager.pulse)
			return;

		if (characterManager.characterAnimation.AnimatorIsPlaying(actionAnimationState))
		{
			characterManager.characterAnimation.ResetAnimationTrigger(actionAnimationState);
			return;
		}

		if (characterManager.characterSound.characterAudioSource.isPlaying)
			return;
		
		characterManager.PlayAnimation(actionAnimationState);
		characterManager.PlaySound();

		TeleportTarget teleportTarget = characterManager.GetComponentInParent<TeleportTarget>();
		if (!teleportTarget) {
			characterManager.PlayEffect();
		}

		if(!characterManager.isSoundReactive)
			canAnimationPlay = false;
	}

	private void ExecuteVoiceInteraction()
	{
		voiceManager.TriggerVoiceEffect();
	}
}

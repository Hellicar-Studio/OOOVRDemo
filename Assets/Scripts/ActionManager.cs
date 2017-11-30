using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
	public TeleportManager teleportManager;
	public TeleportTrigger teleportTrigger;
	public VoiceManager voiceManager;
	public CursorRenderer cursorRenderer;
	public string actionAnimationState = "action";

	protected bool canAnimationPlay = true;

	private CharacterManager lastHitCharacterManager;

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

		if(characterManager != lastHitCharacterManager)
		{
			canAnimationPlay = true;
		}

		lastHitCharacterManager = characterManager;

		if (!characterManager.isSoundReactive ||(characterManager.isSoundReactive && voiceManager.pulse && characterManager.isReadyToReactToSound))
			ExecuteLookInteraction(characterManager);

		TeleportTarget teleportTarget = targetCollider.GetComponentInParent<TeleportTarget>();
		if (!teleportTarget)
			return;

		teleportTrigger.setIncreasing(true);

		cursorRenderer.FadeFromTransparent();

		if (teleportTrigger.triggered)
		{
			ExecuteTeleport(characterManager, teleportTarget.teleportTransform);
			teleportTrigger.startCooldown();
			teleportTrigger.resetPercentage();
		}
	}

	private void ExecuteTeleport(CharacterManager characterManager, Transform teleportTransform)
	{
		teleportManager.DetectTeleportTarget(characterManager, teleportTransform);
		characterManager.PlaySound();
	}

	protected void ExecuteLookInteraction(CharacterManager characterManager)
	{
		if (!canAnimationPlay)
			return;

		if(characterManager.characterAnimation != null)
			if (characterManager.characterAnimation.AnimatorIsPlaying(actionAnimationState))
			{
				characterManager.characterAnimation.ResetAnimationTrigger(actionAnimationState);
				return;
			}

		if (characterManager.characterSound.characterAudioSource.isPlaying)
			return;

		StartCoroutine(characterManager.SoundCooldown());
		
		characterManager.PlayAnimation(actionAnimationState);
		if (teleportManager != null)
			characterManager.PlaySound(teleportManager.currentlyMountedCharacter);
		else
			characterManager.PlaySound();
		characterManager.EnableMoverTriggerState();

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

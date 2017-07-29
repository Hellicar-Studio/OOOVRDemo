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
			HandleActions(hit.collider);
			return;
		}

		cursorRenderer.FadeToTransparent();
		canAnimationPlay = true;
	}

	private void HandleActions(Collider targetCollider)
	{
		CharacterManager characterManager = targetCollider.GetComponentInParent<CharacterManager>();
		if (!characterManager)
			return;

		if (canAnimationPlay)
		{
			ExecuteLookInteraction(characterManager);
			canAnimationPlay = false;
		}

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
		characterManager.PlayAnimation(actionAnimationState);
		characterManager.PlaySound();

		TeleportTarget teleportTarget = characterManager.GetComponentInParent<TeleportTarget>();
		if (teleportTarget)
			return;

		characterManager.PlayEffect();
	}

	private void ExecuteVoiceInteraction()
	{
		voiceManager.TriggerVoiceEffect();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCharacterVisionEffect : MonoBehaviour {

	public Camera cam;
	private CharacterVisionEffectApplier characterVisionEffectApplier;

	void Start()
	{
		characterVisionEffectApplier = cam.GetComponent<CharacterVisionEffectApplier>();
	}

    public void disableAllVisionEffects()
    {
		characterVisionEffectApplier.setCharacterVisionEffect(null);
    }

    public void enablevisionEffect(CharacterManager characterManager)
    {
		characterVisionEffectApplier.setCharacterVisionEffect(characterManager.characterVisionEffect);
    }
}


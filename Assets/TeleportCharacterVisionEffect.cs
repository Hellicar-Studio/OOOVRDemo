using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCharacterVisionEffect : MonoBehaviour {

    private CharacterVisionEffect[] characterVisionEffects;
	public Camera cam;

	void Start()
	{
        characterVisionEffects = cam.GetComponents<CharacterVisionEffect>();
	}

    public void disableAllVisionEffects()
    {
        foreach(CharacterVisionEffect effect in characterVisionEffects)
		{
            effect.enabled = false;
        }
    }

    public void enablevisionEffect(CharacterManager characterManager)
    {
        characterManager.EnableVisionEffect();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCharacterVisionEffect : MonoBehaviour {

    private CharacterVisionEffect[] characterVisionEffects;
	// Use this for initialization
	void Start () {
        GameObject cam = GameObject.Find("CenterEyeAnchor");
        characterVisionEffects = cam.GetComponents<CharacterVisionEffect>();
	}

    public void disableAllVisionEffects()
    {
        foreach(CharacterVisionEffect effect in characterVisionEffects) {
            effect.enabled = false;
        }
    }

    public void enablevisionEffect(CharacterManager characterManager)
    {
        characterManager.EnableVisionEffect();
    }
}

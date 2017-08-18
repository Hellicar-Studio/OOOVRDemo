using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisionEffectApplier : MonoBehaviour {

	public CharacterVisionEffect characterVisionEffect = null;
	public VoiceManager voiceManager;

	public void setCharacterVisionEffect(CharacterVisionEffect _effect)
	{
		characterVisionEffect = _effect;
	}

	protected void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if(characterVisionEffect != null)
		{
			characterVisionEffect.setUniforms();
			Graphics.Blit(source, destination, characterVisionEffect.material);
			return;
		}
		else
		{
			Graphics.Blit(source, destination);
		}

	}
}


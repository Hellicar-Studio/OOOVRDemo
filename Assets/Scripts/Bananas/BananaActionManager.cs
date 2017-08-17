using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaActionManager : ActionManager
{
	public CharacterManager characterManager;

	public void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Banana Guard")
			ExecuteLookInteraction(characterManager);
	}

	void Update()
	{
		if (characterManager.characterAnimation.IsInTransition(0) && characterManager.characterAnimation.IsNameNextState(0, "run"))
		{
			characterManager.characterMover.enableIdleState();
			canAnimationPlay = true;
		}
	}
}


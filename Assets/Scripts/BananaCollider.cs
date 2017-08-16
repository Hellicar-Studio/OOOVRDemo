using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaCollider : MonoBehaviour {

	public CharacterMover characterMover;
	public CharacterAnimation characterAnimation;

	//IEnumerator waitForStandUp()
	//{
	//	while(!(characterAnimation.IsInTransition(0) && characterAnimation.IsNameNextState(0, "run")))
	//	{
	//		yield return null;
	//	}

	//}

	public void OnTriggerEnter(Collider other)
	{
		characterMover.enableTriggerState();
		characterAnimation.TriggerAnimationState("action");
		//StartCoroutine(waitForStandUp());
	}

	void Update()
	{
		if(characterAnimation.IsInTransition(0) && characterAnimation.IsNameNextState(0, "run"))
		{
			characterMover.enableIdleState();
		}
	}
}

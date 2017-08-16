using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour {

	public ForwardMover mover;
	public int triggerState;
	public int idleState;

	public void enableIdleState()
	{
		mover.speed = idleState;
	}

	public void enableTriggerState()
	{
		mover.speed = triggerState;
	}
}

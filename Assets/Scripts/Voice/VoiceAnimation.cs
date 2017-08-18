using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceAnimation : AnimationController
{
	public void PulseVoice()
	{
		animation.SetTrigger("Pulse");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportTrigger : MonoBehaviour {

	[Range(0.0f, 100.0f)]
	public float percentage = 0.0f;

	public float cooldownDuration = 2f;

	public Image indicator;


	[Range(0.0f, 5.0f)]
	public float speedUp;
	[Range(0.0f, 5.0f)]
	public float speedDown;

	[HideInInspector]
	public bool triggered = false;

	private float cooldownStartTime;
	private bool increasing;
	private bool active = true;

	// Use this for initialization
	void Start ()
	{
		
	}

	void Update()
	{
		if (increasing)
			increasePercentage();
		else
			decreasePercentage();

		indicator.fillAmount = percentage / 100.0f;
		increasing = false;
	}

	public void startCooldown()
	{
		if(!active)
		{
			cooldownStartTime = Time.time;
			StartCoroutine(cooldown());
			triggered = false;
		}
	}

	private IEnumerator cooldown()
	{
		while(Time.time - cooldownStartTime < cooldownDuration)
		{
			yield return null;
		}
		active = true;
	}

	public void increasePercentage()
	{
		if(percentage > 100)
		{
			percentage = 100;
		}
		if (percentage < 100)
		{
			percentage += speedUp;
		} else
		{
			active = false;
			triggered = true;
		}
	}

	public void decreasePercentage()
	{
		if(percentage > 0 && !increasing)
		{
			percentage -= speedDown;
		}
		if (percentage < 0)
		{
			percentage = 0;
		}
	}

	public void resetPercentage()
	{
		percentage = 0;
	}

	public void setIncreasing(bool _increasing)
	{
		if (active)
			increasing = _increasing;
		else
			increasing = false;
	}


}

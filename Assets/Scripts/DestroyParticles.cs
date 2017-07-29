using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
	public float duration = 2f;

	void Start()
	{
		Destroy(gameObject, duration);
	}
}

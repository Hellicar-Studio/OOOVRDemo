using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
	public float lifeDuration = 2f;

	void Start()
	{
		Destroy(gameObject, lifeDuration);
	}
}

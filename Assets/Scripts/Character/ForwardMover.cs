using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMover : MonoBehaviour
{
	public Rigidbody rigid;
	public float speed = 2f;
	public float maxDistance = 90f;

	void FixedUpdate()
	{
		rigid.velocity = transform.forward * speed;
	}
}

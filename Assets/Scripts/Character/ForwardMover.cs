using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMover : MonoBehaviour
{
	public Rigidbody rigid;
	public float speed = 2f;
	public float maxDistance = 90f;

	private Vector3 origin;

	void Start()
	{
		origin = transform.position;
	}

	void Update()
	{
		float dist = Vector3.Distance(transform.position, origin);
		if (dist > maxDistance)
			transform.position = origin;
	}

	void FixedUpdate()
	{
		rigid.velocity = transform.forward * speed;
	}
}

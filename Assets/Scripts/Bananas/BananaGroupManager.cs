using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaGroupManager : MonoBehaviour
{

	private Vector3[] allOrigins;
	public GameObject[] allObjects;
	public float maxDistance;

	void Start ()
	{
		allOrigins = new Vector3[allObjects.Length];
		for (int i = 0; i < allObjects.Length; i++)
		{
			allOrigins[i] = allObjects[i].transform.position;
		}
	}
	
	void Update ()
	{
		if (AllObjectsAreBeyondDistance())
		{
			for (int i = 0; i < allObjects.Length; i++)
			{
				allObjects[i].transform.position = allOrigins[i];
			}
		}
	}


	bool AllObjectsAreBeyondDistance()
	{
		float dist;
		for (int i = 0; i < allObjects.Length; i++)
		{
			dist = Vector3.Distance(allObjects[i].transform.position, allOrigins[i]);
			if (dist < maxDistance)
			{
				return false;
			}
		}
		return true;
	}
}


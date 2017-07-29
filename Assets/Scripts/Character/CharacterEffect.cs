using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffect : MonoBehaviour 
{
	public GameObject effectParticles;

	public void SpawnParticles()
	{
		Instantiate(effectParticles, transform.position, Quaternion.identity, transform);
	}
}

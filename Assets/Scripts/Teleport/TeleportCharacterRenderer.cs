using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCharacterRenderer : MonoBehaviour 
{
	public GameObject[] teleportCharacters;

	public void ToggleRenderers(GameObject characterGameObject)
	{
		for (int i = 0; i < teleportCharacters.Length; i++)
		{
			if (teleportCharacters[i].name == characterGameObject.name)
			{
				foreach (var characterRenderer in teleportCharacters[i].GetComponentsInChildren<Renderer>())
				{
					characterRenderer.enabled = false;
				}

				teleportCharacters[i].GetComponentInChildren<Collider>().enabled = false;
			}
			else
			{
				foreach (var characterRenderer in teleportCharacters[i].GetComponentsInChildren<Renderer>())
				{
					characterRenderer.enabled = true;
				}

				teleportCharacters[i].GetComponentInChildren<Collider>().enabled = true;
			}
		}
	}

}

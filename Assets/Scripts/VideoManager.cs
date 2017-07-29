using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour 
{
	public VideoPlayer video;

	private bool isNextSceneLoading;

	private bool HasVideoFinishedPlaying()
	{
		bool isPlaying = false;

		if (video.frame >= (long)video.frameCount - 10 && video.isPrepared)
			isPlaying = true;

		return isPlaying;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.One) || HasVideoFinishedPlaying() && !isNextSceneLoading)
		{
			SkipVideoAndAdvanceScene();
			isNextSceneLoading = true;
		}
	}

	void SkipVideoAndAdvanceScene()
	{
		SceneManager.LoadSceneAsync("Scene_Game");
	}
}

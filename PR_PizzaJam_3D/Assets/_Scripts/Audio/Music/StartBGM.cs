using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBGM : MonoBehaviour {

	[SerializeField] private string music;
	[SerializeField] private bool keepPosition = true;

	private static float currentTime;
	
	void Start() {
		if (!string.IsNullOrEmpty(music)) {
			ServiceLocator.AudioManager.PlayGlobal("Music", music, true);
			if (keepPosition) 
				ServiceLocator.AudioManager.GetGlobalAudioSource().time = currentTime;
		}
	}

	private void Update() {
		AudioSource musicSource = ServiceLocator.AudioManager.GetGlobalAudioSource();
		currentTime = musicSource.time;
	}
}

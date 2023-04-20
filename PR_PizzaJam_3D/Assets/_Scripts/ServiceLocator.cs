using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour {
	// Readonly services
	public static SceneTransition @SceneManager { get; set; }
	public static AudioManager @AudioManager { get; set; }

	// Singleton
	private static ServiceLocator instance;

	private void Awake() {
		if (instance != null && instance != this) {
			Destroy(this);
			return;
		}
		instance = this;
		LocateServices();
	}

	private void LocateServices() {
		@SceneManager = FindObjectOfType<SceneTransition>();
		@AudioManager = FindObjectOfType<AudioManager>();
	}

	private void OnDestroy() {
		if (instance == this) {
			@SceneManager = null;
			AudioManager = null;
		}
	}
}

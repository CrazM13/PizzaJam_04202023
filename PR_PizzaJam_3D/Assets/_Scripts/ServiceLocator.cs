using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour {
	// Readonly services
	public static SceneTransition @SceneManager { get; set; }
	public static AudioManager @AudioManager { get; set; }
	public static SwingManager @SwingManager { get; set; }
	public static PauseMenu @PauseMenu { get; set; }

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
		@SwingManager = new SwingManager();
		@PauseMenu = FindObjectOfType<PauseMenu>();
	}

	private void OnDestroy() {
		if (instance == this) {
			@SceneManager = null;
			@AudioManager = null;
			SwingManager = null;
			@PauseMenu = null;
		}
	}
}

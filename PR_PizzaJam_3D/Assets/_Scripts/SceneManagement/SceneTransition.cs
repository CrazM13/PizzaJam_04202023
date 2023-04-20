using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	[Header("References")]
	[SerializeField] private CanvasGroup canvas;

	[Header("Settings")]
	[SerializeField] private float animationTime;

	private float delayTime;
	private float timeUntilFinished;
	private string changeToScene = "";

	void Awake() {
		canvas.blocksRaycasts = true;
		canvas.alpha = 1;
		Time.timeScale = 0;
		timeUntilFinished = animationTime;
	}

	void Update() {
		if (timeUntilFinished > 0) {

			if (delayTime > 0) {
				delayTime -= Time.unscaledDeltaTime;
				if (delayTime <= 0) {
					canvas.blocksRaycasts = true;
					canvas.alpha = 0;
					Time.timeScale = 0;
				}
			} else {

				timeUntilFinished -= Time.unscaledDeltaTime;

				if (string.IsNullOrEmpty(changeToScene)) {
					canvas.alpha = Mathf.Lerp(0, 1, timeUntilFinished / (animationTime - 0.1f));
				} else {
					canvas.alpha = Mathf.Lerp(1, 0, timeUntilFinished / (animationTime - 0.1f));
				}

				if (timeUntilFinished <= 0) {
					Time.timeScale = 1;
					if (!string.IsNullOrEmpty(changeToScene)) {
						SceneManager.LoadScene(changeToScene);
						timeUntilFinished = animationTime;
						changeToScene = "";
					} else {
						canvas.blocksRaycasts = false;
						canvas.alpha = 0;
					}
				}
			}
		}
	}

	public void LoadSceneByName(string changeToScene) {
		LoadSceneByName(changeToScene, 0);
	}

	public void LoadSceneByName(string changeToScene, float delay) {
		if (timeUntilFinished > 0) return;
		timeUntilFinished = animationTime;
		delayTime = delay;
		this.changeToScene = changeToScene;
		Time.timeScale = 0.3f;

		if (delay <= 0) {
			canvas.blocksRaycasts = true;
			canvas.alpha = 0;
			Time.timeScale = 0;
		}
	}

	public static void LoadScene(string changeToScene, float delay = 0) {
		if (!ServiceLocator.SceneManager) {
			Debug.LogError("No Scene Transition Loaded!");
		} else {
			ServiceLocator.SceneManager.LoadSceneByName(changeToScene, delay);
		}
	}

	public void QuitGame() {
		Application.Quit();
	}

	public static void Quit() {
		ServiceLocator.SceneManager.QuitGame();
	}

}

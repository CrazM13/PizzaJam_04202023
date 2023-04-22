using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffects : MonoBehaviour {

	[Header("Effect Settings")]
	[SerializeField] private string sfxGroup;
	[SerializeField] private AnimationCurve squeezeY;
	[SerializeField] private AnimationCurve squeezeX;

	private float time;
	private bool isPlaying;

	private Vector3 savedScale;

	// Start is called before the first frame update
	void Start() {
		savedScale = transform.localScale;
	}

	// Update is called once per frame
	void Update() {
		if (isPlaying) {
			time += Time.deltaTime;
			transform.localScale = new Vector3(savedScale.x * squeezeX.Evaluate(time), savedScale.y * squeezeY.Evaluate(time), savedScale.z * squeezeX.Evaluate(time));

			if (time >= 1) {
				transform.localScale = savedScale;
				isPlaying = false;
				time = 0;
			}
		}
	}

	public void Play() {
		isPlaying = true;
		ServiceLocator.AudioManager.PlayRandomLocal(transform.position, sfxGroup);
	}
}

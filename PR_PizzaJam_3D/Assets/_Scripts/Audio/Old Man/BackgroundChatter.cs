using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChatter : MonoBehaviour {

	[SerializeField] private float randomness;
	[SerializeField] private float interval;
	[SerializeField] private string audioGroup;

	private float timeUntilChatter;

	private void Start() {
		timeUntilChatter = interval + (Random.value * randomness);
	}

	private void Update() {
		timeUntilChatter -= Time.deltaTime;

		if (timeUntilChatter <= 0) {
			timeUntilChatter += interval + (Random.value * randomness);
			ServiceLocator.AudioManager.PlayRandomLocal(transform.position, audioGroup);
		}
	}

}

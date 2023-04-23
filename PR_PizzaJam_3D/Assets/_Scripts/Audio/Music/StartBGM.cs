using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBGM : MonoBehaviour {

	[SerializeField] private string music;

	
	void Start() {
		if (!string.IsNullOrEmpty(music)) ServiceLocator.AudioManager.PlayGlobal("Music", music, true);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimationManager : MonoBehaviour {

	private const string ACTION_KEY = "Action";
	private const string Hit_KEY = "Hit";
	private const string DEATH_KEY = "Death";

	[SerializeField] private Animator anim;

	public void PlayAction() {
		anim.SetTrigger(ACTION_KEY);
	}

	public void PlayHit() {
		anim.SetTrigger(Hit_KEY);
	}

	public void PlayDeath() {
		anim.SetTrigger(DEATH_KEY);
	}

}

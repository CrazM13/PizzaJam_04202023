using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour {

	[Header("Settings")]
	[SerializeField] private Color defaultColour;
	[SerializeField] private Color aimingColour;

	[Header("Components")]
	[SerializeField] private PlayerController player;
	[SerializeField] private Image crosshair;

	// Update is called once per frame
	void Update() {
		crosshair.color = player.IsAimingAtObject ? aimingColour : defaultColour;
	}
}

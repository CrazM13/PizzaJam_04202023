using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingManager {

	private List<SwingTarget> swingTargets;

	public SwingManager() {
		swingTargets = new List<SwingTarget>();
	}

	public void Register(SwingTarget target) {
		swingTargets.Add(target);
	}

	public void Unregister(SwingTarget target) {
		swingTargets.Remove(target);
	}

	/// <summary>
	/// get all swing targets in area <paramref name="radius"/> around <paramref name="position"/>
	/// </summary>
	/// <param name="position">Center of search area</param>
	/// <param name="radius">Distance to search</param>
	/// <returns></returns>
	public List<SwingTarget> GetTargetsInArea(Vector3 position, float radius) {
		List<SwingTarget> foundObjects = new List<SwingTarget>();

		foreach (SwingTarget target in swingTargets) {
			if (Vector3.Distance(target.transform.position, position) <= radius) {
				foundObjects.Add(target);
			}
		}

		return foundObjects;

	}

	/// <summary>
	/// Send all swing targets in area <paramref name="radius"/> around <paramref name="position"/> hit event
	/// </summary>
	/// <param name="position">Center of search area</param>
	/// <param name="radius">Distance to search</param>
	/// <returns></returns>
	public void SwingAtArea(Vector3 position, float radius) {
		List<SwingTarget> foundTargets = GetTargetsInArea(position, radius);

		foreach (SwingTarget target in foundTargets) target.SendEventHit();
	}

}

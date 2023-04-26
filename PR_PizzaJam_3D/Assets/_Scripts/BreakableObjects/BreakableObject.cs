using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour {

	[SerializeField] private Rigidbody[] rigidbodys;

	private void Start() {
		foreach (Rigidbody rb in rigidbodys) {
			rb.Sleep();
		}
	}

	public void Explode(float force) {
		foreach (Rigidbody rb in rigidbodys) {
			rb.AddExplosionForce(force, transform.position, 1);
		}

		SwingTarget target = GetComponent<SwingTarget>();
		if (target) ServiceLocator.SwingManager.Unregister(target);
	}
}

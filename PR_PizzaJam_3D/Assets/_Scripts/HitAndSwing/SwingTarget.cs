using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwingTarget : MonoBehaviour {

	[SerializeField] private UnityEvent OnHit = new UnityEvent();

	private void Start() {
		ServiceLocator.SwingManager.Register(this);
	}

	public void SendEventHit() {
		OnHit.Invoke();
	}

	private void OnDestroy() {
		ServiceLocator.SwingManager?.Unregister(this);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	#region Inspector
	[Header("Settings")]
	[SerializeField] private float maxMovementSpeed;
	[SerializeField] private AnimationCurve accelerationCurve;
	[SerializeField] private float swingRadius;

	[Header("Components")]
	[SerializeField] private Transform overTheShoulderCamera;
	#endregion

	#region Input
	private Vector2 mouseInput;
	private Vector2 movementInput;
	private bool isSwinging;

	private void GetInputs() {
		mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		isSwinging = Input.GetButtonDown("Fire1");
	}
	#endregion

	#region Movement
	private Rigidbody physicsBody;
	private float movementTime;
	private float currentSpeed;

	private Vector3 currentMovementDirection;

	private void InitMovement() {
		physicsBody = GetComponent<Rigidbody>();
	}

	private void OnMove() {
		if (movementInput.x != 0 || movementInput.y != 0) {
			movementTime += Time.deltaTime;

			currentSpeed = maxMovementSpeed * accelerationCurve.Evaluate(movementTime);

			currentMovementDirection = cameraTransform.TransformDirection(new Vector3(movementInput.x, 0, movementInput.y));
			currentMovementDirection = new Vector3(currentMovementDirection.x, 0, currentMovementDirection.z).normalized;

			Vector3 movementVector = new Vector3(currentMovementDirection.x * currentSpeed, 0, currentMovementDirection.z * currentSpeed);
			
			Vector3 targetPosition = transform.position + movementVector;
			physicsBody.MovePosition(targetPosition);

		} else {
			currentSpeed = 0;
			movementTime = 0;
		}
	}
	#endregion

	#region Camera
	private Transform cameraTransform;
	private Transform modelTransform;

	private void InitCamera() {
		Cursor.lockState = CursorLockMode.Locked;
		cameraTransform = Camera.main.transform;
		modelTransform = transform.Find("Model");
	}

	private void OnUpdateCamera() {
		modelTransform.Rotate(Vector3.up, mouseInput.x);
		overTheShoulderCamera.Rotate(Vector3.right, -mouseInput.y);
	}
	#endregion

	#region Swinging
	private void OnSwingUpdate() {
		if (isSwinging) {
			ServiceLocator.SwingManager.SwingAtArea(cameraTransform.position + (cameraTransform.forward * swingRadius), swingRadius);
		}
	}
	#endregion

	// Start is called before the first frame update
	void Start() {
		InitCamera();
		InitMovement();
	}

	// Update is called once per frame
	void Update() {
		GetInputs();

		OnMove();
		OnUpdateCamera();
		OnSwingUpdate();
	}
}

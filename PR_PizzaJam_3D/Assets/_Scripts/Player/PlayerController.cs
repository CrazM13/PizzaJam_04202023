using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	#region Inspector
	[Header("Movement Settings")]
	[SerializeField] private float maxMovementSpeed;
	[SerializeField] private AnimationCurve accelerationCurve;
	[Header("Swing Settings")]
	[SerializeField] private float swingRadius;
	[SerializeField] private int swingDistance;
	[Header("Camera Settings")]
	[SerializeField] private Vector2 cameraSensativity;
	[SerializeField] private float maxCameraTilt;
	[Space()]
	[Header("Model Components")]
	[SerializeField] private Transform rotationTransform;
	[SerializeField] private HumanoidAnimationManager animations;
	[Header("Camera Components")]
	[SerializeField] private Transform overTheShoulderCamera;
	[SerializeField] private Transform cameraFocusPoint;
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

	private float cameraTilt;

	private void InitCamera() {
		Cursor.lockState = CursorLockMode.Locked;
		cameraTransform = Camera.main.transform;
	}

	private void OnUpdateCamera() {
		rotationTransform.Rotate(Vector3.up, mouseInput.x * cameraSensativity.x * Time.deltaTime);

		cameraTilt += -mouseInput.y * cameraSensativity.y * Time.deltaTime;
		cameraTilt = Mathf.Clamp(cameraTilt, -maxCameraTilt, maxCameraTilt);
		//Vector3 direction = Quaternion.AngleAxis(cameraTilt, cameraTransform.right) * rotationTransform.forward;
		//cameraFocusPoint.position = rotationTransform.position + direction;

		overTheShoulderCamera.localRotation = Quaternion.AngleAxis(cameraTilt, Vector3.right);
	}
	#endregion

	#region Swinging

	public bool IsAimingAtObject { get; private set; }

	private void OnSwingUpdate() {

		Vector3 swingCenter = cameraTransform.position + (cameraTransform.forward * swingRadius);

		IsAimingAtObject = CanSwingRaycast();//ServiceLocator.SwingManager.GetTargetsInArea(swingCenter, swingRadius).Count > 0;

		if (isSwinging) {
			SwingInRaycast();
			ServiceLocator.AudioManager.PlayRandomLocal(transform.position, "SwingSFX");
			animations.PlayAction();
		}
	}

	private bool CanSwingRaycast() {
		for (int i = 1; i <= swingDistance; i++) {
			Vector3 swingCenter = cameraTransform.position + (cameraTransform.forward * (swingRadius * i));
			if (ServiceLocator.SwingManager.GetTargetsInArea(swingCenter, swingRadius).Count > 0) return true;
		}
		return false;
	}

	private void SwingInRaycast() {
		for (int i = 1; i <= swingDistance; i++) {
			Vector3 swingCenter = cameraTransform.position + (cameraTransform.forward * (swingRadius * i));
			ServiceLocator.SwingManager.SwingAtArea(swingCenter, swingRadius);
		}
	}

#if UNITY_EDITOR

	private void DebugSwingRaycast() {
		if (cameraTransform) {
			for (int i = 1; i <= swingDistance; i++) {
				Vector3 swingCenter = cameraTransform.position + (cameraTransform.forward * (swingRadius * i));
				Gizmos.DrawWireSphere(swingCenter, swingRadius);
			}
		}
	}

#endif

	#endregion

	// Start is called before the first frame update
	void Start() {
		InitCamera();
		InitMovement();
	}

	// Update is called once per frame
	void Update() {
		GetInputs();

		
		OnSwingUpdate();
	}

	private void FixedUpdate() {
		OnMove();
		OnUpdateCamera();
	}

#if UNITY_EDITOR
	private void OnDrawGizmos() {
		DebugSwingRaycast();
	}
#endif
}

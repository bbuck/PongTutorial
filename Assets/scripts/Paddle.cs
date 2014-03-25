using UnityEngine;
using System.Collections;

public enum ScreenSide {
	Left, Right
}

[RequireComponent(typeof(Motor))]
[RequireComponent(typeof(BoxCollider2D))]
public class Paddle : MonoBehaviour {

	public float speed = 3.0f;
	public float wallOffset = 0.1f;
	public ScreenSide side = ScreenSide.Left;

	protected Motor _motor;

	void Awake() {
		SetInitialPosition();
	}

	void Start() {
		OnStart();
	}

	void Update() {
		OnUpdate();
	}

	#region helper functions

	protected void OnGamePaused(bool pauseState) {
		renderer.enabled = !pauseState;
	}

	protected void SetInitialPosition() {
		float cameraDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
		Vector3 centerPoint = Vector3.zero;
		if (side == ScreenSide.Left) {
			centerPoint = Camera.main.ViewportToWorldPoint(new Vector3(wallOffset, 0.5f, cameraDistance));
		} else {
			centerPoint = Camera.main.ViewportToWorldPoint(new Vector3(1.0f - wallOffset, 0.5f, cameraDistance));
		}

		transform.position = centerPoint;
	}

	protected void CheckBounds() {
		Vector3 halfSize = renderer.bounds.size / 2.0f;
		Vector3 topRight = Camera.main.WorldToViewportPoint(renderer.bounds.center + halfSize);
		Vector3 bottomLeft = Camera.main.WorldToViewportPoint(renderer.bounds.center - halfSize);

		Vector2 velocity = _motor.TargetVelocity;

		if (topRight.y > 1 && _motor.TargetVelocity.y > 0) {
			velocity.y = 0;
		} else if (bottomLeft.y < 0 && _motor.TargetVelocity.y < 0) {
			velocity.y = 0;
		}

		_motor.TargetVelocity = velocity;
	}

	protected virtual void OnUpdate() { /* empty */ }

	protected virtual void OnStart() {
		_motor = GetComponent<Motor>();
	}

	#endregion 
}

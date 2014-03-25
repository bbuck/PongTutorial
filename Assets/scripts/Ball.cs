using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Motor))]
[RequireComponent(typeof(CircleCollider2D))]
public class Ball : MonoBehaviour {

	#region properties

	public float startSpeed = 5.0f;
	public float maxSpeed = 10.0f;
	public float speedIncr = 5.0f;

	public float maxDegreeReflection = 70.0f;

	public AudioClip blipSound;
	public AudioClip blopSound;

	private bool playBlop = true;
	private float curSpeed = 0.0f;
	private Vector2 _direction;
	private Motor _motor;

	#endregion

	void Awake() {
		ResetPosition();
	}

	void Start() {
		_motor = GetComponent<Motor>();
		_direction = Vector2.zero;
	}

	void Update() {
		_motor.TargetVelocity = Vector2.zero;

		if (!GameManager.Instance.Paused && curSpeed > 0.0f) {
			TestCameraCollision();
		
			// Keep the balls speed static
			_motor.TargetVelocity = _direction * curSpeed;
		} 
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "paddle" && collision.contacts.Length > 0) {
			Transform paddle = collision.gameObject.transform;
			Vector2 paddleCenter = paddle.renderer.bounds.center;
			
			// Determine radian angle adjustment from center of paddle.
			float radiansPerDistance = (maxDegreeReflection * Mathf.Deg2Rad) / (paddle.renderer.bounds.size.y / 2.0f);

			Vector2 collPoint = collision.contacts[0].point;
			
			float distFromCenter = Mathf.Abs(collPoint.y - paddleCenter.y);
			float radAdjustment = distFromCenter * radiansPerDistance;
			float radAngle = 0.0f;
			if (paddle.GetComponent<Paddle>().side == ScreenSide.Left) {
				if (collPoint.y > paddleCenter.y) {
					radAngle = radAdjustment;
				} else {
					radAngle = (2 * Mathf.PI) - radAdjustment;
				}
				PlaySound(blipSound);
			} else {
				if (collPoint.y > paddleCenter.y) {
					radAngle = Mathf.PI - radAdjustment;
				} else {
					radAngle = Mathf.PI + radAdjustment;
				}
				PlaySound(blopSound);
			}
			_direction = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
			IncrementSpeed();
		}
	}

	#region public methods
	
	public void StartMoving() {
		_direction = RandomDirection();
		curSpeed = startSpeed;
	}
	
	public void ResetPosition() {
		Vector3 viewportPoint = new Vector3(0.5f, 0.5f, Mathf.Abs(Camera.main.transform.position.z - transform.position.z));
		transform.position = Camera.main.ViewportToWorldPoint(viewportPoint);
		
		curSpeed = 0.0f;
	}
	
	#endregion

	#region helper methods

	void OnGamePaused(bool pauseState) {
		renderer.enabled = !pauseState;
	}

	void IncrementSpeed() {
		curSpeed += speedIncr;

		if (curSpeed > maxSpeed) {
			curSpeed = maxSpeed;
		}
	}

	void TestCameraCollision() {
		Vector3 halfSize = renderer.bounds.size / 2.0f;
		Vector3 topRight = Camera.main.WorldToViewportPoint(renderer.bounds.center + halfSize);
		Vector3 bottomLeft = Camera.main.WorldToViewportPoint(renderer.bounds.center - halfSize);

		if (topRight.x > 1) {
			ScoreSystem.Instance.PlayerScored();
			curSpeed = 0.0f;
			ResetPosition();
			StartMoving();
		} else if (bottomLeft.x < 0) {
			ScoreSystem.Instance.EnemyScored();
			curSpeed = 0.0f;
			ResetPosition();
			StartMoving();
		} 

		if (topRight.y > 1) {
			_direction.y = -Mathf.Abs(_direction.y);
			IncrementSpeed();
			PlaySound();
		} else if (bottomLeft.y < 0) {
			_direction.y = Mathf.Abs(_direction.y);
			IncrementSpeed();
			PlaySound();
		}
	}

	void PlaySound(AudioClip clip) {
		AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
	}

	void PlaySound() {
		if (playBlop) {
			PlaySound(blopSound);
		} else {
			PlaySound(blipSound);
		}

		playBlop = !playBlop;
	}

	Vector2 RandomDirection() {
		float angle;
		if (Random.Range(0, 1) == 0) {
			angle = Random.Range(110, 250) * Mathf.Deg2Rad;
		} else {
			angle = Random.Range(290, 430) * Mathf.Deg2Rad;
		}
		float x = Mathf.Cos(angle);
		float y = Mathf.Sin(angle);

		return new Vector2(x, y);
	}

	#endregion
}

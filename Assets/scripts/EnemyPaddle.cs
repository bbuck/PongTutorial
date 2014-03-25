using UnityEngine;
using System.Collections;

public class EnemyPaddle : Paddle {

	private uint framesUpdated = 0;

	#region helper methods

	protected override void OnUpdate() {
		// Reset movement each frame.
		_motor.TargetVelocity = Vector2.zero;

		if (!GameManager.Instance.Paused) {
			if (framesUpdated % 8 != 0) {
				TrackBall();
			}

			++framesUpdated;

			CheckBounds();
		}
	}

	protected override void OnStart() {
		base.OnStart();
	}

	void TrackBall() {
		Vector3 targetPos = GetClosestBall().renderer.bounds.center;
		Vector3 halfSize = renderer.bounds.size / 2.0f;
		Vector3 center = renderer.bounds.center;
		Vector3 topRight = center + halfSize;
		Vector3 bottomLeft = center - halfSize;

		Vector2 velocity = Vector2.zero;

		if (targetPos.y > center.y) {
			velocity.y = speed;
		} else if (targetPos.y < center.y) {
			velocity.y = -speed;
		}

		if (targetPos.y < topRight.y && targetPos.y > bottomLeft.y) {
			velocity.y = 0;
		}

		_motor.TargetVelocity = velocity;
	}

	Ball GetClosestBall() {
		GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
		if (balls.Length > 0) {
			// Find the closest ball
			Ball closest = null;
			float curDistance = -1;;
			foreach (GameObject obj in balls) {
				Ball temp = obj.GetComponent<Ball>();
				if (temp != null) {
					float tempDistance = Vector3.Distance(transform.position, temp.transform.position);
					if (curDistance < 0) {
						curDistance = tempDistance;
						closest = temp;
					} else if (tempDistance < curDistance) {
						curDistance = tempDistance;
						closest = temp;
					}
				}
			}

			return closest;
		}

		return null;
	}

	#endregion
}

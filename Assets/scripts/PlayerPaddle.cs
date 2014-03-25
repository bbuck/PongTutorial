using UnityEngine;
using System.Collections;

public class PlayerPaddle : Paddle {

	#region helpermethods

	protected override void OnUpdate() {
		Vector2 velocity = Vector2.zero;

		if (Input.GetKeyDown(KeyCode.P)) {
			GameManager.Instance.Pause();
		}

		if (!GameManager.Instance.Paused) {
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
				velocity.y = speed;
			} else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
				velocity.y = -speed;
			}

			_motor.TargetVelocity = velocity;

			CheckBounds();
		} else {
			_motor.TargetVelocity = velocity;
		}
	}

	#endregion
}

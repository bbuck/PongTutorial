using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Motor : MonoBehaviour {

	public float maxVelocityChange = 10.0f;

	[HideInInspector]
	public Vector2 TargetVelocity { get; set; }

	// Use this for initialization
	void Start() {
		TargetVelocity = Vector2.zero;
	}
	
	void FixedUpdate() {
		Vector2 velocity = rigidbody2D.velocity;
		velocity = (TargetVelocity - velocity);
		velocity = velocity.normalized * Mathf.Clamp(velocity.magnitude, 0, maxVelocityChange);

		rigidbody2D.AddForce(velocity, ForceMode.VelocityChange);
	}
}

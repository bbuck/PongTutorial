using UnityEngine;
using System.Collections;

public class TitleScreenController : MonoBehaviour {
	public TextMesh continueMesh;

	void Start() {
		PerformSetup();
	}

	void Update () {
		if (Input.anyKeyDown) {
			Application.LoadLevel("MainGame");
		}
	}

	#region helper methods

	void PerformSetup() {
		float cameraDistance = Mathf.Abs(Camera.main.transform.position.z - 0);
		Vector3 location = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, cameraDistance));

		continueMesh.transform.position = location;
	}

	#endregion
}

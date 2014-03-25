using UnityEngine;
using System.Collections;

public class WinScreenController : MonoBehaviour {

	public TextMesh winnerMesh;
	public TextMesh continueMesh;

	public float continueTime = 5.0f;

	private bool canContinue = false;

	void Start() {
		PerformSetup();
	}

	void Update() {
		if (canContinue) {
			if (Input.anyKeyDown) {
				Application.LoadLevel("MainGame");
			}
		}
	}

	#region helper methods

	IEnumerator NotifyCanContinue() {
		yield return new WaitForSeconds(continueTime);
		continueMesh.renderer.enabled = true;
		canContinue = true;
	}

	void PerformSetup() {
		continueMesh.renderer.enabled = false;

		if (GameState.GameWinner == Player.User) {
			winnerMesh.text = "You Win!";
		} else {
			winnerMesh.text = "Computer Wins!";
		}

		StartCoroutine(NotifyCanContinue());
	}

	#endregion
}

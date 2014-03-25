using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour {

	private static ScoreSystem _instance;
	public static ScoreSystem Instance {
		get {
			if (_instance == null) {
				GameObject global = GameObject.Find("global");
				if (global != null) {
					_instance = global.GetComponent<ScoreSystem>();
				}
			}

			return _instance;
		}
	}

	public TextMesh playerScoreMesh;
	public TextMesh enemyScoreMesh;

	private int playerScore = 0;
	private int enemyScore = 0;

	void Start() {
		PositionScores();
	}

	#region public methods

	public void PlayerScored() {
		++playerScore;
		if (playerScore == 10) {
			GameState.GameWinner = Player.User;
			Application.LoadLevel("WinScreen");
		} else {
			playerScoreMesh.text = playerScore.ToString();
		}
	}

	public void EnemyScored() {
		++enemyScore;
		if (enemyScore == 10) {
			GameState.GameWinner = Player.Computer;
			Application.LoadLevel("WinScreen");
		} else {
			enemyScoreMesh.text = enemyScore.ToString();
		}
	}

	#endregion

	#region helper methods

	void PositionScores() {
		float cameraDistance = Mathf.Abs(Camera.main.transform.position.z - -1);
		Vector3 playerPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.3f, 0.9f, cameraDistance));
		Vector3 enemyPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.7f, 0.9f, cameraDistance));

		playerScoreMesh.transform.position = playerPoint;
		playerScoreMesh.renderer.sortingLayerName = "background";
		enemyScoreMesh.transform.position = enemyPoint;
		enemyScoreMesh.renderer.sortingLayerName = "background";
	}

	#endregion
}

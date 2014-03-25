using UnityEngine;
using System.Collections;

public delegate void PauseEventHandler(bool pauseState);

public class GameManager : MonoBehaviour {
	public event PauseEventHandler GamePaused;

	private static GameManager _instance;
	public static GameManager Instance {
		get {
			if (_instance == null) {
				GameObject global = GameObject.Find("global");
				if (global != null) {
					_instance = global.GetComponent<GameManager>();
				}
			}

			return _instance;
		}
	}

	public bool Paused { get; private set; }

	private bool started = false;

	void Awake() {
		Paused = false;
	}

	void Update() {
		if (!started) {
			GameObject.Find("ball").GetComponent<Ball>().StartMoving();
			started = true;
		}
	}

	#region public methods

	public void Pause() {
		Paused = !Paused;
		if (GamePaused != null) {
			GamePaused(Paused);
		}
	}

	#endregion
}

using UnityEngine;
using System.Collections;

public enum Visibility {
	Visible, Hidden
}

[RequireComponent(typeof(SpriteRenderer))]
public class PauseVisibility : MonoBehaviour {
	public Visibility pauseVisibility = Visibility.Hidden;

	void OnEnable() {
		GameManager.Instance.GamePaused += OnGamePaused;
		if (pauseVisibility == Visibility.Visible) {
			renderer.enabled = false;
		}
	}

	void OnDestroy() {
		GameManager.Instance.GamePaused -= OnGamePaused;
	}

	#region helper methods

	void OnGamePaused(bool pauseState) {
		renderer.enabled = (pauseVisibility == Visibility.Visible ? pauseState : !pauseState);
	}

	#endregion
}

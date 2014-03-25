using UnityEngine;
using System.Collections;

public enum Player {
	User, Computer, None
}

public class GameState {
	public static Player GameWinner = Player.None;
}

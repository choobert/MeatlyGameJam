using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void onPlayGame(int levelToPlay) {
		Debug.Log("Pressed Play Game");
		Application.LoadLevel(levelToPlay);
	}
}

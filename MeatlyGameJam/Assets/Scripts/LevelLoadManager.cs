using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class LevelLoadManager : MonoBehaviour {

	private GameManager gm;
	
	private Canvas hud;
	private Canvas mainMenu;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this);		
		
		gm = GameManager.Instance;
		gm.OnStateChange += HandleOnStateChange;
		
		hud = GameObject.Find ("HUD").GetComponent<Canvas>();
		DontDestroyOnLoad(hud);
		
		mainMenu = GameObject.Find ("MainMenu").GetComponent<Canvas>();
		DontDestroyOnLoad(mainMenu);
		
		// first level SHOULD be the Main Menu
		gm.setGameState(GameState.MainMenu);
		hud.enabled = false;
	}

	void HandleOnStateChange ()
	{
		Debug.Log("Handling state change to: " + gm.gameState);
	}
	
	void OnLevelWasLoaded(int aLevel) {
		Debug.Log ("Loaded Level " + aLevel);
		
		if (aLevel > 0) {
			Level cl = gm.getCurrentLevel();
			cl.initSpots();
			gm.setGameState(GameState.Game);
			
			hud.enabled = true;
			mainMenu.enabled = false;
			
			GameManager.player = GameObject.Find("Player").GetComponent<Player>();
		}
		else {
			gm.setGameState(GameState.MainMenu);
			
			hud.enabled = false;
			mainMenu.enabled = true;
		}
	}
	
}

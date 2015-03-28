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
			
			gm.getCurrentLevel().spawnEnemiesAndIdeas();
			
			if ( aLevel < 3 ) {
				HUD._instance.updateLevelDescription("Home Dev");
			}
			else if ( aLevel < 5) {
				HUD._instance.updateLevelDescription("Indie Dev");
			}			
			else {
				HUD._instance.updateLevelDescription("AAA Dev");
				
				// since we just loaded the last level we need to reset all of the quests
				gm.resetQuests();
			}			
		}
		else {
			gm.setGameState(GameState.MainMenu);
			
			hud.enabled = false;
			mainMenu.enabled = true;
		}
	}
	
}

using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public enum GameState {MainMenu, Game}
public delegate void OnStateChangeHandler();

public class GameManager : ScriptableObject {

	private static GameManager _instance;
	public event OnStateChangeHandler OnStateChange;
	
	public static Player player;
	
	private static Level[] levels;
	private static int currentLevel;
	
	public static int ideaCount 	{ get; private set; }
	public static int bugCount 		{ get; private set; }
	public static int gameCount 	{ get; private set; }
	
	public GameState gameState { get; private set; }
	
	protected GameManager() {}	
	
	public static GameManager Instance {
		get {
			if (GameManager._instance == null) {
				GameManager._instance = ScriptableObject.CreateInstance<GameManager>();
				
				DontDestroyOnLoad(GameManager._instance);
				
				//Instantiate some values
				currentLevel = 0;
				levels = new Level[5];
				
				// Home
				levels[0] = ScriptableObject.CreateInstance<Level>();
				levels[0].init(1, 3, 3, 5, 1, 2);
				
				// Imagaination Land
				levels[1] = ScriptableObject.CreateInstance<Level>();
				levels[1].init(2, 3, 3, 5, 0, 0);
				levels[1].initEnemiesAndIdeas(5, 3, 0);
				
				// InDee Games
				levels[2] = ScriptableObject.CreateInstance<Level>();
				levels[2].init(3, 5, 3, 7, 3, 4);
				
				// Rabbit Hole
				levels[3] = ScriptableObject.CreateInstance<Level>();
				levels[3].init(4, 5, 3, 7, 2, 2); 
				levels[3].initEnemiesAndIdeas(0, 0, 4);
				
				// Too Big To Fail Corp
				levels[4] = ScriptableObject.CreateInstance<Level>();
				levels[4].init(5, 0, 0, 99, 0, 0);
			}
			
			return GameManager._instance;
		}
	}
	
	public void setGameState(GameState aGameState) {
		gameState = aGameState;
		
		if(OnStateChange != null) {
			OnStateChange();
		}
	}
	
	public void collectIdea(int aValue) {
		ideaCount += aValue;
		
		string dispStr = "Collected Idea!";
		
		if (ideaCount >= levels[currentLevel].ideasPerGame) {
			ideaCount = 0;
			bugCount = 0;
			gameCount++;
			
			dispStr = "Completed a Game!";
			
			if (gameCount >= getCurrentLevel().gamesForQuest) {
				dispStr = "Completed all Games!";
				currentLevel = getCurrentLevel().questLevelIndex;
				Application.LoadLevel (getCurrentLevel().levelIndex);
			}
		}
		
		HUD._instance.updateCountDisplay(ideaCount, bugCount, gameCount, dispStr);
	}
	
	public void collectBug(int aValue) {
		bugCount += aValue;
		
		string dispStr = "Encountered Bug!";
		Debug.Log ("BugCount: " + bugCount + ", BugsPerGame: " + getCurrentLevel().bugsPerGame);
				
		if (bugCount >= getCurrentLevel().bugsPerGame) {
			bugCount = 0;
			
			if (ideaCount > 0) {
				ideaCount = 0;
				
				dispStr = "Ideas Ruined!";
			}
			else if (gameCount > 0) {
				gameCount--;
				
				dispStr = "Game Ruined!";
			}
		}
		
		HUD._instance.updateCountDisplay(ideaCount, bugCount, gameCount, dispStr);
	}
	
	public void enterQuest() {
		currentLevel = getCurrentLevel().questLevelIndex;
		
		Debug.Log("Loading level: " + currentLevel);
		
		Application.LoadLevel (getCurrentLevel().levelIndex);
	}
	
	public void completeLevel() {
	
		Debug.Log ("Currently on level: " + currentLevel);
	
		currentLevel = getCurrentLevel().nextLevelIndex;
		
		Debug.Log("Loading level: " + currentLevel);
		
		Application.LoadLevel (getCurrentLevel().levelIndex);
	}
	
	public bool completeQuest() {
		return gameCount >= getCurrentLevel().gamesForQuest;
	}
	
	public void enablePlayer() {
		player.setMoveEnabled(true);
	}
	
	public void disablePlayer() {
		player.setMoveEnabled(false);
	}
	
	public Level getCurrentLevel() {
		return levels[currentLevel];
	}
	
	public bool getQuestComplete() {
		return getCurrentLevel().questComplete;
	}
	
	public void updateQuest() {
		Debug.Log ("Updating quest");
	
		if (!getCurrentLevel().questComplete) {
			Debug.Log ("Have not already completed.");
			Debug.Log ("Have " + gameCount + ", need " + getCurrentLevel().gamesForQuest);
			int reqGames = getCurrentLevel().gamesForQuest;		
			if (gameCount >= reqGames) {
				gameCount = 0; // reset the number of games the player has
				HUD._instance.updateCountDisplay(ideaCount, bugCount, gameCount, ""); // turn in our games
							
				getCurrentLevel().questComplete = true; // change the dialogue from the npc
				
				// Allow the player to walk past
				BoxCollider2D bc = GameObject.Find("LevelBlocker").GetComponent<BoxCollider2D>();
				bc.enabled = false;
			}
		}
	}
	
	public void resetQuests() {
		for (int i = 0; i < levels.Length; i++) {
			levels[i].questComplete = false;
		}
	}
	
}
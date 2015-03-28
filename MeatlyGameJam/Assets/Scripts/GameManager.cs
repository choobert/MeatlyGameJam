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
				levels = new Level[3];
				
				levels[0] = ScriptableObject.CreateInstance<Level>();
				levels[0].init(1, 3, 2, 5, 1, 2);
				
				levels[1] = ScriptableObject.CreateInstance<Level>();
				levels[1].init(2, 3, 2, 5, 0, 0);
				
				levels[2] = ScriptableObject.CreateInstance<Level>();
				levels[2].init(3, 5, 3, 7, 0, 0);
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
				Application.LoadLevel (Application.loadedLevel -1);
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
		currentLevel = levels[currentLevel].nextLevelIndex;
		Application.LoadLevel (currentLevel);
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
		Debug.Log ("levels: " + levels.Length);
		return levels[currentLevel];
	}
	
	public bool getQuestComplete() {
		return getCurrentLevel().questComplete;
	}
	
	public void updateQuest() {
		if (!getCurrentLevel().questComplete) {
			
			int reqGames = getCurrentLevel().gamesForQuest;		
			if (gameCount >= reqGames) {
				gameCount = 0; // reset the number of games the player has
				
				BoxCollider2D bc = GameObject.Find("LevelBlocker").GetComponent<BoxCollider2D>();
				bc.enabled = false;
			}
		}
	}
	
}
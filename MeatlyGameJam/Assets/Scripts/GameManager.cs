using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public enum GameState {MainMenu, Game};
public delegate void OnStateChangeHandler();

public class GameManager : ScriptableObject {

	private static GameManager _instance;
	public event OnStateChangeHandler OnStateChange;
	
	private static Player player;
	
	private static Text ideaText;
	private static Text bugText;
	private static Text gameText;
	
	private static int ideaCount 	= 0;
	private static int bugCount 	= 0;
	private static int gameCount 	= 0;
	
	private static int ideasPerGame	= 10;
	private static int bugsPerGame	= 3;
	private static int gamesForQuest = 5;
	
	public GameState gameState { get; private set; }
	
	protected GameManager() {}
	
	
	public static GameManager Instance {
		get {
			if (GameManager._instance == null) {
				GameManager._instance = ScriptableObject.CreateInstance<GameManager>();
				
				player = GameObject.Find("Player").GetComponent<Player>();
				
				DontDestroyOnLoad(GameManager._instance);
				
				// Save the GUI FOR-EV-OR
				GameObject idea = GameObject.Find("IdeaCount");
				DontDestroyOnLoad(idea);
				ideaText = idea.GetComponent<Text>();
				
				GameObject bug = GameObject.Find("BugCount");
				DontDestroyOnLoad(bug);
				bugText = bug.GetComponent<Text>();
				
				GameObject game = GameObject.Find("GameCount");
				DontDestroyOnLoad(game);
				gameText = game.GetComponent<Text>();
			}
			
			return GameManager._instance;
		}
	}
	
	public void SetGameState(GameState aGameState) {
		gameState = aGameState;
		
		if(OnStateChange != null) {
			OnStateChange();
		}
	}
	
	public void collectIdea(int aValue) {
		ideaCount += aValue;
		
		if (ideaCount >= ideasPerGame) {
			ideaCount = 0;
			bugCount = 0;
			gameCount++;
		}
		
		updateCountDisplay();
	}
	
	public void collectBug(int aValue) {
		bugCount += aValue;
		
		
		
		if (bugCount >= bugsPerGame) {
		
			Debug.Log("BugCount: " + bugCount + ", BugsPerGame: " + bugsPerGame);
			bugCount = 0;
			ideasPerGame = 0;
			
			if (ideaCount > 0) {
				ideaCount = 0;
			}
			else if (gameCount > 0) {
				gameCount--;
			}
		}
		
		updateCountDisplay();
	}
	
	public bool completeQuest() {
		return gameCount >= gamesForQuest;
	}
	
	private void updateCountDisplay() {
		ideaText.text	= ideaCount.ToString();
		bugText.text 	= bugCount.ToString();
		gameText.text	= gameCount.ToString();
	}
	
	public void enablePlayer() {
		player.setMoveEnabled(true);
	}
	
	public void disablePlayer() {
		player.setMoveEnabled(false);
	}
}

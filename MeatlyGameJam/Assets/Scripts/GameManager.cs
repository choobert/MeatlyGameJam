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
	
	private static int ideasPerGame = 10;
	private static int bugsPerGame 	= 3;
	private static int gameForQuest = 5;
	
	public GameState gameState { get; private set; }
	
	protected GameManager() {}
	
	
	public static GameManager Instance {
		get {
			if (GameManager._instance == null) {
				GameManager._instance = ScriptableObject.CreateInstance<GameManager>();
				
				player = GameObject.Find("Player").GetComponent<Player>();
				ideaText = GameObject.Find("IdeaCount").GetComponent<Text>();
				bugText = GameObject.Find("BugCount").GetComponent<Text>();
				gameText = GameObject.Find("GameCount").GetComponent<Text>();
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
			bugsPerGame = 0;
			gameCount++;
		}
		
		updateCountDisplay();
	}
	
	public void collectBug(int aValue) {
		bugCount -= aValue;
		
		if (bugCount >= bugsPerGame) {
			bugCount = 0;
			ideasPerGame = 0;
			gameCount--;
			
			if(gameCount < 0) {
				gameCount = 0;
			}
		}
		
		updateCountDisplay();
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

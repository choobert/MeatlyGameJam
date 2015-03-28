using UnityEngine;
using System.Collections;

public class Level : ScriptableObject {

	public int levelIndex;

	public int ideasPerGame;
	public int bugsPerGame;
	public int gamesForQuest;
	
	public int questLevelIndex;
	public int nextLevelIndex;
	
	public bool questComplete = false;
	
	private int numEnemies = 0;
	private int numIdeas = 0;
	private int numFreefall = 0;
	
	public static IdeaSpot[] ideaSpots;
	public static BugSpot[] bugSpots;
	public static FreeFallSpot[] freefallSpots;
	
	public void init (int aLevelIndex, int aIdeasPerGame, int aBugsPerGame, int aGamesForQuest, int aQuestLevelIndex, int aNextLevelIndex) {
		levelIndex = aLevelIndex;
		
		ideasPerGame = aIdeasPerGame;
		bugsPerGame = aBugsPerGame;
		gamesForQuest = aGamesForQuest;
		
		questLevelIndex = aQuestLevelIndex;
		nextLevelIndex = aNextLevelIndex;
	}
	
	public void initEnemiesAndIdeas(int aEnemies, int aIdeas, int aFreefall) {
		numEnemies = aEnemies;
		numIdeas = aIdeas;
		numFreefall = aFreefall;
	}
	
	public void initSpots() {
		ideaSpots = GameObject.FindObjectsOfType<IdeaSpot>();
		bugSpots = GameObject.FindObjectsOfType<BugSpot>();
		freefallSpots = GameObject.FindObjectsOfType<FreeFallSpot>();
	}
	
	public void ideaCollected() {
	
		Debug.Log ("Number idea spots: " + ideaSpots.Length);
	
		IdeaSpot newIdeaSpot = ideaSpots[Random.Range(0, ideaSpots.Length)];
		Instantiate(Resources.Load ("Idea"), newIdeaSpot.transform.position, Quaternion.identity);
	}
	
	public void bugEncountered() {
		BugSpot newBugSpot = bugSpots[Random.Range(0, ideaSpots.Length)];
		Instantiate(Resources.Load ("Bug"), newBugSpot.transform.position, Quaternion.identity);
	}
	
	public void createRandomFreeFall() 
	{		
		int r = Random.Range(0, 3);
		FreeFallSpot newSpot = freefallSpots[Random.Range(0, freefallSpots.Length)];
		
		if (r < 2) {
			Instantiate(Resources.Load ("FreefallIdea"), newSpot.transform.position, Quaternion.identity);
		}
		else {
			Instantiate(Resources.Load ("FreefallBug"), newSpot.transform.position, Quaternion.identity);
		}
	}
	
	public void spawnEnemiesAndIdeas() {
		for (int i=0; i < numEnemies; i++) {
			BugSpot newBugSpot = bugSpots[Random.Range(0, ideaSpots.Length)];
			Instantiate(Resources.Load ("Bug"), newBugSpot.transform.position, Quaternion.identity);
		}
		
		for (int j=0; j < numIdeas; j++) {
			IdeaSpot newIdeaSpot = ideaSpots[Random.Range(0, ideaSpots.Length)];
			Instantiate(Resources.Load ("Idea"), newIdeaSpot.transform.position, Quaternion.identity);
		}
		
		for (int k=0; k < numFreefall; k++) {
			createRandomFreeFall();
		}
	}
}

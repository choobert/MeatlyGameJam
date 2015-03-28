using UnityEngine;
using System.Collections;

public class Level : ScriptableObject {

	public int gamesForQuest;
	public int questIndex;
	public int nextLevelIndex;
	
	public bool questComplete = false;
	
	public static IdeaSpot[] ideaSpots;
	public static BugSpot[] bugSpots;
	
	public void init (int aGamesForQuest, int aQuestIndex, int aNextLevelIndex) {
		gamesForQuest = aGamesForQuest;
		questIndex = aQuestIndex;
		nextLevelIndex = aNextLevelIndex;
	}
	
	public void initSpots() {
		ideaSpots = GameObject.FindObjectsOfType<IdeaSpot>();
		bugSpots = GameObject.FindObjectsOfType<BugSpot>();
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
}

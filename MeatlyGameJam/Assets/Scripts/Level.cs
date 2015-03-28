using UnityEngine;
using System.Collections;

public class Level : ScriptableObject {

	private static Level _instance;
	
	public static IdeaSpot[] ideaSpots;
	
	protected Level() {}
	
	public static Level Instance {
		get {
			if (Level._instance == null) {
				Level._instance = ScriptableObject.CreateInstance<Level>();
				
				ideaSpots = GameObject.FindObjectsOfType<IdeaSpot>();
			}
			
			return Level._instance;
		}
	}
	
	public void ideaCollected() {
	
		Debug.Log ("Creating new Idea, idea spawn location length: " + ideaSpots.Length);
		
		int rand = Random.Range(0, ideaSpots.Length);
		Debug.Log ("Selecting a random number: " + rand);
	
		IdeaSpot newIdeaSpot = ideaSpots[rand];
		GameObject newIdea = (GameObject) Instantiate(Resources.Load ("Idea"), newIdeaSpot.transform.position, Quaternion.identity);
	}
}

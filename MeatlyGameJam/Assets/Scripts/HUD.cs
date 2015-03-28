using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class HUD : MonoBehaviour {

	private static Text alertText;
	private static Text dialogueText;
	private static Text levelText;
	
	private static Text ideaText;
	private static Text bugText;
	private static Text gameText;
	
	public static HUD _instance;
	
	// Use this for initialization
	void Start () {
		HUD._instance = this;
		
		alertText 	= GameObject.Find("AlertMessage").GetComponent<Text>();
		levelText 	= GameObject.Find("LevelDescription").GetComponent<Text>(); 
		ideaText 	= GameObject.Find("IdeaCount").GetComponent<Text>();
		bugText 	= GameObject.Find("BugCount").GetComponent<Text>();
		gameText 	= GameObject.Find("GameCount").GetComponent<Text>();
	}
	
	public void updateCountDisplay(int aIdeaCount, int aBugCount, int aGameCount, string aAlertMessage) {
		ideaText.text	= aIdeaCount.ToString();
		bugText.text 	= aBugCount.ToString();
		gameText.text	= aGameCount.ToString();
		
		StartCoroutine(displayAlert(aAlertMessage));
	}
	
	public IEnumerator displayAlert(string aAlertMessage) {
		if (alertText == null) {
			alertText = GameObject.Find ("AlertMessage").GetComponent<Text>();
		}
		
		alertText.text = aAlertMessage;		
		yield return new WaitForSeconds(1);
		alertText.text = "";
	}
	
	public void showDialogue(string aMessage) {
		if (dialogueText == null) {
			dialogueText = GameObject.Find("DialogueBox").GetComponent<Text>();
		}
		
		dialogueText.text = aMessage;
	}
	
	public void updateLevelDescription(string aLevel) {
		levelText.text = aLevel;
	}
}

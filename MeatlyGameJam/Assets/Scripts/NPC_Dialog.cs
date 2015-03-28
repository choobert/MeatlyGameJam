using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Xml;

public class NPC_Dialog : MonoBehaviour {

	
	public Text dialogText;
	public GameObject questObject;
	public string xmlFileName;

	private bool isTalking = false;	
	private bool isTextScrolling = false;
	
	private string[] questLines;
	private string[] completeLines;
	
	private int currentLine = 0;
	private int textSpeed = 2;
	
	void Start() {
	
		TextAsset textAsset = (TextAsset) Resources.Load (xmlFileName);
	
		XmlDocument xmlDoc = new XmlDocument();	
		xmlDoc.LoadXml (textAsset.text);
		
		XmlNodeList xnList = xmlDoc.SelectNodes("scene/quest/line");
		questLines = new string[xnList.Count];
		for(int i=0; i < xnList.Count; i++) {
			questLines[i] = xnList[i].InnerText;
		}
		
		xnList = xmlDoc.SelectNodes("scene/complete/line");
		completeLines = new string[xnList.Count];
		for(int i=0; i < xnList.Count; i++) {
			completeLines[i] = xnList[i].InnerText;
		}
	}
	
	void Update () {
		if (isTalking) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (isTextScrolling) {
					// display the full line
					isTextScrolling = false;
					dialogText.text = questLines[currentLine];
				}
				else {
					if (currentLine < questLines.Length - 1) {
						currentLine++;
						isTextScrolling = true;
						StartCoroutine(scrollText());
					}
					else {
						currentLine = 0;
						isTalking = false;
						isTextScrolling = false;
						
						// clear out the dialogue
						dialogText.text = "";
						
						// draw the path to the quest
						questObject.GetComponent<Renderer>().enabled = true;
						questObject.GetComponent<PolygonCollider2D>().enabled = true;
						
						// allow the player to move again
						GameManager.Instance.enablePlayer();
					}
				}
			}
		}
	}
	
	void OnTriggerEnter2D (Collider2D aCollider) {
	
		if (aCollider.gameObject.tag == "Player") {
			GameManager.Instance.disablePlayer();
		
			isTalking = true;
			isTextScrolling = true;
		
			StartCoroutine(scrollText());
		}
	}
	
	IEnumerator scrollText() {
	
		string displayText = "";
		
		for (int i = 0; i < questLines[currentLine].Length && isTextScrolling; i++) {
			displayText += questLines[currentLine][i];
			//Debug.Log (displayText);
			dialogText.text = displayText;
			yield return new WaitForSeconds(1/textSpeed);		
		}
		
		isTextScrolling = false;
	}
}

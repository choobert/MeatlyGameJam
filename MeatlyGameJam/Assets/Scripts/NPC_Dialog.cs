using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Xml;

public class NPC_Dialog : MonoBehaviour {

	
	public Text dialogText;

	private bool isTalking = false;	
	private bool isTextScrolling = false;
	
	private string[] talkLines;
	
	private int currentLine = 0;
	private int textSpeed = 2;
	
	void Start() {
	
		TextAsset textAsset = (TextAsset) Resources.Load ("Dialog");
	
		XmlDocument xmlDoc = new XmlDocument();	
		xmlDoc.LoadXml (textAsset.text);
	
		XmlNodeList nodeList = xmlDoc.GetElementsByTagName("scene");
		
		foreach (XmlNode sceneNode in nodeList)
		{
			// get actors
			foreach (XmlNode actorNode in sceneNode.ChildNodes)
			{
				// Get their lines
				talkLines = new string[actorNode.ChildNodes.Count];
				foreach(XmlNode lineNode in actorNode.ChildNodes)
				{
					int lineNum = Convert.ToInt16(lineNode.Attributes["id"].Value);
					talkLines[lineNum] = lineNode.InnerText;
				}
			}
		}
	}
	
	void Update () {
		if (isTalking) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (isTextScrolling) {
					// display the full line
					isTextScrolling = false;
					dialogText.text = talkLines[currentLine];
				}
				else {
					if (currentLine < talkLines.Length - 1) {
						currentLine++;
						isTextScrolling = true;
						StartCoroutine(scrollText());
					}
					else {
						currentLine = 0;
						isTalking = false;
						isTextScrolling = false;
						
						dialogText.text = "";
					}
				}
			}
		}
	}
	
	void OnTriggerEnter2D () {
		isTalking = true;
		isTextScrolling = true;
		
		StartCoroutine(scrollText());
	}
	
	void OnTriggerExit2D () {
		 isTalking = false;
	}
	
	IEnumerator scrollText() {
	
		string displayText = "";
		
		for (int i = 0; i < talkLines[currentLine].Length && isTextScrolling; i++) {
			displayText += talkLines[currentLine][i];
			dialogText.text = displayText;
			yield return new WaitForSeconds(1/textSpeed);		
		}
		
		isTextScrolling = false;
	}
}

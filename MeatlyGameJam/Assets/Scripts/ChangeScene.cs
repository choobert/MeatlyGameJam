using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	protected void OnTriggerEnter2D (Collider2D aCollider) {
	
		Debug.Log("Enter quest world");
		GameManager.Instance.enterQuest();
	}
}

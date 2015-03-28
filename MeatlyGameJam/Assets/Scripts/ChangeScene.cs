using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	protected void OnTriggerEnter2D (Collider2D aCollider) {
		GameManager.Instance.enterQuest();
	}
}

using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public int nextSceneIndex = 0;
	
	protected void OnTriggerEnter2D (Collider2D aCollider) {
		Application.LoadLevel (nextSceneIndex);
	}
}

using UnityEngine;
using System.Collections;

public class FreefallObject : MonoBehaviour {
		
		public float fallSpeed = 10f;
		
		private Rigidbody2D myRigidbody;
		private bool isGrounded = false;
		
		// Use this for initialization
		void Start () {
			myRigidbody = GetComponent<Rigidbody2D>();
		}
		
		void FixedUpdate () {
			if(!isGrounded) {
				myRigidbody.velocity = new Vector2(0, -1 * fallSpeed );
			}
		}
		
		void OnCollisionEnter2D(Collision2D hit) {
			if (hit.gameObject.tag != "Player") {
				isGrounded = true;
				StartCoroutine(DestroyObject());
			}
		}
		
		IEnumerator DestroyObject() {
			float delay = 0.01f;
			
			for (int i = 0; i < 10; i++) {
				transform.localScale += new Vector3(0.01f, 0.01f);
				yield return new WaitForSeconds(delay);
			}
			
			GameManager.Instance.getCurrentLevel().createRandomFreeFall();
			
			Debug.Log("OK lets destroy it now.");
			Destroy (this.gameObject);
		}
	}
	
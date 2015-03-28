using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour {

	private Rigidbody2D myRigidbody;
	
	public int movementSpeed = 2;
	private int velocity = 1;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate () {
		myRigidbody.velocity = new Vector2(velocity * movementSpeed, myRigidbody.velocity.y );
	}
	
	// Turn around if hit our boundary
	void OnTriggerEnter2D (Collider2D aCollider) {
		if (aCollider.gameObject.tag == "BugBoundary") {
			velocity *= -1;
		}
	}
}

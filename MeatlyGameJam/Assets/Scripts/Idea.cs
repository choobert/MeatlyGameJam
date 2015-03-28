using UnityEngine;
using System.Collections;

public class Idea : MonoBehaviour {

	public float movementSpeed = 0.5f;

	private Rigidbody2D myRigidbody;
	private int velocity = 1;
	
	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate () {
		myRigidbody.velocity = new Vector2(0, velocity * movementSpeed );
	}
	
	// Turn around if hit our boundary
	void OnTriggerEnter2D (Collider2D aCollider) {
		if (aCollider.gameObject.tag == "IdeaBoundary") {
			velocity *= -1;
		}
	}
}

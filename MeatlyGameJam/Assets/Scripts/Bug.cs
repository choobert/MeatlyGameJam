using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour {

	public int movementSpeed = 2;
	public Sprite leftSprite;
	public Sprite rightSprite;

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
		myRigidbody.velocity = new Vector2(velocity * movementSpeed, myRigidbody.velocity.y );
	}
	
	// Turn around if hit our boundary
	void OnTriggerEnter2D (Collider2D aCollider) {
		if (aCollider.gameObject.tag == "BugBoundary") {
			velocity *= -1;
			
			if (velocity < 0) {
				GetComponent<SpriteRenderer>().sprite = leftSprite;
			}
			else {
				GetComponent<SpriteRenderer>().sprite = rightSprite;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public int movementSpeed = 10;
	public float jumpForce = 10.0f;
	
	private Rigidbody2D myRigidbody;
	private Animator anim;
	private int animVelocity;
	
	private bool isJumping = false;
	private bool isGrounded = false;
	
	void Awake () {
		myRigidbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		animVelocity = Animator.StringToHash("Velocity");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (isGrounded) {
				isJumping = true;
				isGrounded = false;
			}
		}
	}
	
	void FixedUpdate () {
		float lHorizontal = Input.GetAxisRaw("Horizontal");
		anim.SetInteger(animVelocity, (int) lHorizontal);
		
		myRigidbody.velocity = new Vector2(lHorizontal * movementSpeed, myRigidbody.velocity.y );
			
		if(isJumping)
		{
			myRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
			isJumping = false;
		}
	}
	
	void OnCollisionEnter2D(Collision2D hit) {
		if(hit.gameObject.tag == "Ground")
			isGrounded = true;
	}
}

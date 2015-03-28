using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int movementSpeed = 10;
	public float jumpForce = 10.0f;
	
	private GameManager gm;
	
	private Rigidbody2D myRigidbody;
	private Animator anim;
	private int animVelocity;
	
	private bool isMoveEnabled = true;
	private bool isJumping = false;
	private bool isGrounded = false;
	
	public int ideaCount = 0;
	public int bugCount = 0;
	public int gameCount = 0;
	
	void Awake () {
	
		gm = GameManager.Instance;
		
		myRigidbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		animVelocity = Animator.StringToHash("Velocity");
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoveEnabled && Input.GetKeyDown(KeyCode.Space)) {
			if (isGrounded) {
				isJumping = true;
				isGrounded = false;
			}
		}
	}
	
	void FixedUpdate () {
		float lHorizontal = isMoveEnabled ? Input.GetAxisRaw("Horizontal") : 0;
		anim.SetInteger(animVelocity, (int) lHorizontal);
		
		myRigidbody.velocity = new Vector2(lHorizontal * movementSpeed, myRigidbody.velocity.y );
			
		if(isJumping)
		{
			myRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
			isJumping = false;
		}
		
		Vector3 cameraPosition = this.gameObject.transform.position;
		cameraPosition.z = -10;
		Camera.main.transform.position = cameraPosition;
	}
	
	void OnCollisionEnter2D(Collision2D hit) {
		if(hit.gameObject.tag == "Ground") {
			isGrounded = true;
		}
		else if(hit.gameObject.tag == "Idea") {
			gm.collectIdea(1);
			Destroy(hit.gameObject);
			Level.Instance.ideaCollected();
		}
		else if(hit.gameObject.tag == "Bug") {
			gm.collectBug(1);
			Destroy (hit.gameObject);
		}
	}
	
	public void setMoveEnabled(bool aBool) {
		isMoveEnabled = aBool;
	}
}

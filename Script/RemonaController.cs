using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; // Allows for use of UI namespace inclusindText

// ----- !! TO DO !! -----
// -- find out why Shesometimes gets stuck on the floor. dodgy colision?, floor misplaces, code updating incorrectly? 

// 1. Sort out code
// 		1.1. Into functions
//		1.2. Cleaned up 
// 		1.3. More Useful names and notations
//
// 2. Impliment player actions
// 		2.1 Impliment Jump function
// 		2.2 Impliment Attack
// 		2.3 Impliment ONE jump after Attack Unless another 
//			Attack follows and the player is Below the roof
// 

public class RemonaController : CoinControl 
{
	//--------------------------------
	//	-- Player Actions --
	//
	private bool attack = false;
	public float Jumpforce = 90.0f; 		// Make Fully jump later 
	public float moveSpeed = 3.0f;  		// Mvement speed

	//--------------------------------
	//		-- Checks --
	//

	public Transform groundCheckTransform;  // Stored the trabsform for the ground
	private bool isGrounded = false;					// Bool to check if the player is grounded
	public LayerMask GroundCheckLayerMask;  // ---???--- | find out what this should be tagged for 


	// -----------------------------
	// 		-- Score --
	public Text scoreText; 		 //Store Ref o the UI Text component
	public int score;			 //Int to store the players Score

	// ------------------------------
	// 		-- Game States -- 
	private bool gameOver;
	private bool restart;

	Animator animator;			 // !!!! Check to see if the animator needs caching !!!!
	Rigidbody2D rb;

	// Use this for initialization
	void Start ()
	{
		gameOver = false;
		restart = false;

		//Cache the animator
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

		CoinSpawn ();
		score = 0;
		SetScoreText ();
	}
		
	void FixedUpdate() // for phisics only does once
	{
		if(Input.GetKey("s"))
		{
			//attack = true;
			//animator.SetBool ("Attack", attack);
			animator.SetTrigger("Attack");
			attack = true;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		CoinRemove (rb);
		if (restart) 
		{
			
			if (Input.GetKeyDown ("r")) 
			{
				moveSpeed = 3f;
				gameOver = false;
				restart = false;
			}
		}

		if (gameOver) 
		{
			moveSpeed = 0f;
			restart = true;
		}
			
		//Calls the player movement code to speed up, slow down and default speed
		PlayerMovement ();  

		//If the player is grounded then Jump can be activates 
		if (isGrounded) 		
		{
			//Calls the player Jump function to initiate a jump
			PlayerJump ();  
		}

		//Check is the player is grounded
		IsGrounded (); 
	}
		
	// Check if the player is grounded
	void IsGrounded()
	{
		//Checks if a collision has taken place with the ground
		isGrounded = Physics2D.OverlapCircle 
			(groundCheckTransform.position, 0.1f,GroundCheckLayerMask);

		//Sets the bool To grounded for the jump function and the animator
		animator.SetBool("Grounded", isGrounded);
		//attack = false;
	}

	void PlayerJump() //Player jump function
	{
		// Impliment jump if grounded or right after a successful attack
		// Jump should use force jumpForce

		// If attack is successful / if an enemy is hit
		// Allow jump for 1s ish with Timer

		// Test code 
		//Adding force to the player when jetpack is active 

		if (Input.GetButton ("Fire1"))				//Checks for Button "Fire1" AKA: mouse button 1
		{
			Vector2 newVelocity = rb.velocity;		//Define a new Vector2 to hold the current velocity
			newVelocity.y = Jumpforce;				//Apply the JumpForce to the relevent part of the Vector2 "Y"
			rb.velocity = newVelocity;				//Makes the velocity equal to the new velocity just calculated
			isGrounded = false;						// Set grounded to false to disable Double jumping
		}

	}

	void PlayerAttack()
	{



		// allow the player to attack at any time for funsies
		// play attack animation
		// checked for damage
		// ?? enable attack jump?? 
		attack = false;
	}

	void PlayerMovement() // Enables the player to move bac kand forth
	{
		// Insert player movments here to clean up code
		// ----TO DO----
		// the Camera follows the player so the camera will need its own speed and these will need some perametersto indicate the edges of the stage

		if (Input.GetKey ("d")) 				// - Speed up
		{
			Vector2 newVelocity = rb.velocity;  // Define velocity
			newVelocity.x = moveSpeed+3; 		// Make velocity == Move speed
			rb.velocity = newVelocity; 			// Assign the Velocity
		} 
		else if (Input.GetKey ("a")) 			// - Slow down
		{
			Vector2 newVelocity = rb.velocity;  // Define velocity
			newVelocity.x = -moveSpeed; 		// Make velocity == Move speed
			rb.velocity = newVelocity; 			// Assign the Velocity
		} 
		else 									// - Default movement
		{
			Vector2 newVelocity = rb.velocity;  // Define velocity
			newVelocity.x = moveSpeed; 			// Make velocity == Move speed
			rb.velocity = newVelocity; 			// Assign the Velocity
		}
	}

	//Collisions
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("PickUp"))
		{
			Destroy (other.gameObject);
			score++;
			SetScoreText ();
		}

		if (other.gameObject.CompareTag ("Enemy"))
		{
			if (!attack) {
				// YOU Dead
				//Destroy (other.gameObject);
				scoreText.text = "Yu ded! Press - 'R'";
				gameOver = true;
			} 
			else if (attack)
			{
				if (other.gameObject.CompareTag ("Enemy")) 
				{
					Destroy (other.gameObject);
					score += 20;
					SetScoreText ();
					attack = false;
				}
			}
		}
	}


	void SetScoreText()
	{
		scoreText.text = "Score: " + score.ToString ();
	}

}

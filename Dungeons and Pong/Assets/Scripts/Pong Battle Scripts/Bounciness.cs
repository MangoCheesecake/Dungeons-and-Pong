using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bounciness : MonoBehaviour {
	// Ball and gameplay variables
	public float ballVelocity = 2700;
	Rigidbody2D rb2d;
	bool isPlay;
	int randInt;


	// Function called at the very start of the game.
	void Awake () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		randInt = Random.Range (1, 11);
	}

	void Start(){
		
	}

	/* Function called at every change of the frame. Updates the position of the ball, 
	 * the position of the paddle. Starts the game at the click of the mouse. */
	void Update () {
		if(rb2d.transform.position.x < -2000 || rb2d.transform.position.x > 2000){
			isPlay = false;
			rb2d.velocity = new Vector2 (0, 0);
			if (Input.GetKey (KeyCode.R)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			}
		}
		if (Input.GetMouseButton(0) == true && isPlay == false){
			transform.parent = null;
			isPlay = true;
			rb2d.transform.position = new Vector2 (0, 0);
			rb2d.isKinematic = false;
			if(randInt <= 3){
				rb2d.AddForce(new Vector2(ballVelocity, ballVelocity));
			}
			if(randInt > 3 && randInt <= 6){
				rb2d.AddForce(new Vector2(-ballVelocity, -ballVelocity));
			}
			if (randInt > 6) {
				rb2d.AddForce (new Vector2 (ballVelocity, -ballVelocity));
			}

		}
	}
	// Function called for every collision between 2D objects.
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Brick") { // Destroy bricks on contact.
			Debug.Log ("Brick Hit");
			Destroy (col.gameObject);
		}
	}
}

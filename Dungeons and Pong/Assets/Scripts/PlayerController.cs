using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{

	public float movementSpeed = 1;
	public bool canMove = true;

	private Rigidbody2D rbody;
	private Animator anim;

	public static bool playerExists;

	void Start () 
	{
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		canMove = true;

		//if player already exists, destroy new one made

		DontDestroyOnLoad(transform.gameObject);

		if (!playerExists) 
		{
			playerExists = true;
		} 
		else 
		{
			Destroy (gameObject);
		}
	}

	void FixedUpdate () 
	{
		//gets vertical and horizontal keyboard inputs
		Vector2 movement_vector = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		//if there are inputs, sends inputs to animator
		if ( movement_vector != Vector2.zero && canMove)
		{
			anim.SetBool("isWalking", true);
			anim.SetFloat("input_x", movement_vector.x);
			anim.SetFloat("input_y", movement_vector.y);

			//moves character based on inputs
			rbody.MovePosition (rbody.position + movement_vector * Time.deltaTime * movementSpeed);
		}
		else 
		{
			anim.SetBool("isWalking", false);
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour 
{
	//how fast the monster moves
	public float movementSpeed;
	//keeps track of whether or not the monster is moving
	private bool isWalking;

	public bool canMove = true;

	//how long the monster should wait and move
	public float toWait;
	public float toMove;

	//counters for toWait and toMove
	private float wait;
	private float move;


	private Vector2 movement_vector;

	Rigidbody2D rbody;
	Animator anim;

	void Start () 
	{
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		RandomMoving ();

	}

	void FixedUpdate () 
	{
		if (isWalking && canMove)
		{
			move -= Time.deltaTime;
			rbody.MovePosition (rbody.position + movement_vector * Time.deltaTime *movementSpeed);

			if (move < 0f)
			{
				isWalking = false;
				wait = Random.Range(toWait * 0.75f, toWait * 1.25f);
			}
		}
		else
		{
			wait -= Time.deltaTime;
			movement_vector = Vector2.zero;
			if (wait < 0f)
			{
				isWalking = true;
				move = Random.Range(toMove * 0.75f, toMove * 1.25f);
				movement_vector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
			}

		}

		if ( movement_vector != Vector2.zero )
		{
			anim.SetBool("isWalking", true);
			anim.SetFloat("input_x", movement_vector.x);
			anim.SetFloat("input_y", movement_vector.y);
		}
		else 
		{
			anim.SetBool("isWalking", false);
		}
	}

	void RandomMoving()
	{
		//randomly determines if the monster starts off moving or standing still
		if (Random.Range (0, 2) == 0) 
		{
			isWalking = false;
			wait = Random.Range (toWait * 0.50f, toWait * 1.50f);
		} 
		else 
		{
			isWalking = true;
			move = Random.Range(toMove * 0.75f, toMove * 1.25f);
			movement_vector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
		}

	}


}

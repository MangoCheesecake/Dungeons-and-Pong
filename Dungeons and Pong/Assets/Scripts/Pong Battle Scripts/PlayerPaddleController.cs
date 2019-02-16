using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddleController : MonoBehaviour 
{
	public bool isAlive;
	public float speed;
	public float yAxis;

	public Vector2 paddlePos;
	public Vector2 ballPos;

	Vector2 targetPos;
	public GameObject ball;
	private Vector3 playerPos;

	void Start () 
	{
		if (transform.parent.GetComponent<Character> ().id == 0 && transform.parent.GetComponent<Character>().characterName == "Player") 
		{
			GetComponent<AIPaddleController> ().enabled = false;
		} 
		else 
		{
			GetComponent<PlayerPaddleController> ().enabled = false;
		}

		isAlive = true;
		speed = transform.parent.GetComponent<Character>().mobility/20; 
		yAxis = transform.position.y;
	}

	void Update () 
	{
		if (isAlive) 
		{
			float xPos = transform.position.x + (Input.GetAxis("Horizontal") * speed);
			playerPos = new Vector3 (Mathf.Clamp (xPos, -7.5f, 5.5f), yAxis, 0f);
			transform.position = playerPos;
		}
	}
}

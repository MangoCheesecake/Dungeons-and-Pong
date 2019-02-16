using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddleController : MonoBehaviour 
{
	public bool isAlive;
	public float speed;
	public float colLeft;
	public float colRight;
	public float yAxis;

	public Vector2 paddlePos;
	public Vector2 ballPos;

	Vector2 targetPos;
	public GameObject ball;

	void Start () 
	{
		isAlive = true;
		speed = transform.parent.GetComponent<Character>().mobility; 
		colLeft = transform.position.x - 0.763f;
		colRight = transform.position.x + 0.763f;
		yAxis = transform.position.y;
	}

	void Update () 
	{
		if (ball != null && isAlive) 
		{
			paddlePos = new Vector2 ( Mathf.Clamp(transform.position.x,colLeft,colRight), yAxis);
			ballPos = new Vector2 (Mathf.Clamp (ball.transform.position.x, colLeft, colRight), yAxis);

			targetPos = Vector2.Lerp (paddlePos, ballPos, Time.deltaTime * speed);
			gameObject.transform.position = targetPos;
		}
	}
}

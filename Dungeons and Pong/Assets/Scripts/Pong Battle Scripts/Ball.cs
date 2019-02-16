using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour 
{
	public float ballVelocity;
	Rigidbody2D rb2d;
	bool isPlay;
	int randInt;
	public Text text;


	HealthBar healthBar;
	Brick brick;
	Paddle paddle;
	float damage;

	public GameObject[] balls;

	void Start () 
	{
		balls = GameObject.FindGameObjectsWithTag("Ball");

		foreach (GameObject ball in balls) 
		{
			Physics2D.IgnoreCollision(ball.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());
		}

		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		randInt = Random.Range (1, 11);


		if(randInt <= 3)
		{
			rb2d.AddForce(new Vector2(ballVelocity, ballVelocity));
		}
		if(randInt > 3 && randInt <= 6)
		{
			rb2d.AddForce(new Vector2(-ballVelocity, -ballVelocity));
		}
		if (randInt > 6) 
		{
			rb2d.AddForce (new Vector2 (ballVelocity, -ballVelocity));
		}
	}

	void Update()
	{
		transform.rotation = Quaternion.identity;
		if (rb2d.velocity.y <= 0 && rb2d.velocity.y > -1) 
		{
			rb2d.AddForce (new Vector2 (0, -50));
		}
		if (rb2d.velocity.y > 0 && rb2d.velocity.y < 1) 
		{
			rb2d.AddForce (new Vector2 (0, 50));
		}

		if (rb2d.velocity.x <= 0 && rb2d.velocity.x > -1) 
		{
			rb2d.AddForce (new Vector2 (-50, 0));
		}
		if (rb2d.velocity.x > 0 && rb2d.velocity.x < 1) 
		{
			rb2d.AddForce (new Vector2 (50, 0));
		}


		rb2d.velocity = new Vector2 (Mathf.Clamp (rb2d.velocity.x, -7, 7), Mathf.Clamp (rb2d.velocity.y, -7, 7));
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "HealthBar") 
		{ 
			healthBar = col.gameObject.GetComponent<HealthBar> ();
			damage = 20;
			healthBar.TakeDamage (20);

		}
		if (col.gameObject.tag == "Blocks") 
		{ 
			brick = col.gameObject.GetComponent<Brick> ();
			damage = brick.currentHealth * 0.9f + 0.1f;
			brick.TakeDamage (damage);
		}
		if (col.gameObject.tag == "Paddle") 
		{ 
			paddle = col.gameObject.GetComponent<Paddle> ();
			damage = paddle.currentHealth * 0.9f + 0.1f;
			paddle.TakeDamage(damage);
		}
	}

}

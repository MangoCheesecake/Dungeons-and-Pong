using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour 
{
	public float maxHealth;
	public float currentHealth;
	public float recovery;
	public bool isAlive;

	private SpriteRenderer paddle;
	public SpriteRenderer head;

	public Sprite white;
	public Sprite red;
	public Sprite orange;
	public Sprite yellow;
	public Sprite green;
	public Sprite blue;
	public Sprite purple;
	public Sprite violet;

	Character stats;
	AIPaddleController aiPaddleController;
	PlayerPaddleController playerPaddleController;




	void Start()
	{
		isAlive = true;
		paddle = GetComponent<SpriteRenderer> ();
		stats = transform.parent.GetComponent<Character> (); 
		head = transform.GetChild (0).GetComponent<SpriteRenderer>();

		SetHealth (stats.mana_capacity/9);
		SetSprite (currentHealth);
		recovery = stats.mana_recovery;
	}

	void Update()
	{
		currentHealth = Mathf.Clamp(currentHealth += recovery * Time.deltaTime, 0, maxHealth);
		SetSprite (currentHealth);
		if (isAlive == false) 
		{
			paddle.color = new Color(1f,1f,1f, Mathf.Clamp(currentHealth/maxHealth,0,0.5f));
			head.color = new Color(1f,1f,1f, Mathf.Clamp(currentHealth/maxHealth,0,0.5f));
			if (currentHealth == maxHealth) 
			{
				recovery *= 2;
				isAlive = true;
				paddle.color = new Color (1f, 1f, 1f, 1f);
				head.color = new Color(1f,1f,1f, 1f);
				GetComponent<BoxCollider2D> ().isTrigger = false;
				GetComponent<AIPaddleController> ().isAlive = true;
				GetComponent<PlayerPaddleController> ().isAlive = true;
			} 
		}
	}

	public void SetHealth(float health)
	{
		maxHealth = health;
		currentHealth = health;
	}

	public void TakeDamage(float amount)
	{
		currentHealth -= amount;
		SetSprite (currentHealth);

		if (currentHealth <= 0) 
		{
			IsDead ();
		}
	}

	public void SetSprite(float curHealth)
	{
		if (curHealth <= 1) 
		{
			paddle.sprite = white;
		}
		if (curHealth > 1 && curHealth <= 10)
		{
			paddle.sprite = red;
		}
		if (curHealth > 10 && curHealth <= 100)
		{
			paddle.sprite = orange;
		}
		if (curHealth > 100 && curHealth <= 1000)
		{
			paddle.sprite = yellow;
		}
		if (curHealth > 1000 && curHealth <= 10000)
		{
			paddle.sprite = green;
		}
		if (curHealth > 10000 && curHealth <= 100000) 
		{
			paddle.sprite = blue;
		}
		if (curHealth > 100000 && curHealth <= 1000000)
		{
			paddle.sprite = purple;
		}
		if (curHealth > 1000000)
		{
			paddle.sprite = violet;
		}
	}

	public void IsDead()
	{
		isAlive = false;
		GetComponent<BoxCollider2D> ().isTrigger = true;
		GetComponent<AIPaddleController> ().isAlive = false;
		GetComponent<PlayerPaddleController> ().isAlive = false;
		paddle.color = new Color(1f,1f,1f,0f);
		head.color = new Color(1f,1f,1f,0f);
		recovery /= 2;
	}
}

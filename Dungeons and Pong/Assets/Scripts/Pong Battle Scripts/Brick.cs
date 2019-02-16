using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour 
{
	public float maxHealth;
	public float currentHealth;

	private SpriteRenderer sr;

	public Sprite white;
	public Sprite red;
	public Sprite orange;
	public Sprite yellow;
	public Sprite green;
	public Sprite blue;
	public Sprite purple;
	public Sprite violet;

	Character stats;

	void Start()
	{
		sr = GetComponent<SpriteRenderer> ();
		stats = transform.parent.transform.parent.GetComponent<Character> (); 
		SetHealth (stats.mana_capacity / 9);
		SetSprite (currentHealth);
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
			sr.sprite = white;
		}
		if (curHealth > 1 && curHealth <= 10)
		{
			sr.sprite = red;
		}
		if (curHealth > 10 && curHealth <= 100)
		{
			sr.sprite = orange;
		}
		if (curHealth > 100 && curHealth <= 1000)
		{
			sr.sprite = yellow;
		}
		if (curHealth > 1000 && curHealth <= 10000)
		{
			sr.sprite = green;
		}
		if (curHealth > 10000 && curHealth <= 100000) 
		{
			sr.sprite = blue;
		}
		if (curHealth > 100000 && curHealth <= 1000000)
		{
			sr.sprite = purple;
		}
		if (curHealth > 1000000)
		{
			sr.sprite = violet;
		}
	}

	public void IsDead()
	{
		Destroy (gameObject);
	}

}

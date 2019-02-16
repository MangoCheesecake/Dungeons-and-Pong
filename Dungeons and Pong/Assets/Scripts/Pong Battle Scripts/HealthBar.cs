using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour 
{
	public GameObject character; 
	public Slider healthSlider;
	public float maxHealth;
	public float currentHealth;
	public bool isAlive;

	Character stats;
	public GameCondition gameCondition;

	public void Start()
	{
		stats = transform.parent.transform.parent.GetComponent<Character> (); 
		gameCondition = GameObject.Find ("Game Manager").GetComponent<GameCondition> ();
		SetHealth (stats.vitality);
	}

	public void SetHealth(float health)
	{
		isAlive = true;
		maxHealth = health;
		currentHealth = health;
		healthSlider.maxValue = maxHealth;
		healthSlider.value = currentHealth;
	}

	public void TakeDamage(float amount)
	{
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		if (currentHealth <= 0 && isAlive) 
		{
			isAlive = false;
			IsDead ();
		}
	}

	public void IsDead()
	{
		stats.isAlive = false;
		Destroy(transform.parent.parent.Find ("Paddle").gameObject);
		Destroy (transform.parent.parent.Find ("Bricks").gameObject);
		gameCondition.characterDied();
	}
}

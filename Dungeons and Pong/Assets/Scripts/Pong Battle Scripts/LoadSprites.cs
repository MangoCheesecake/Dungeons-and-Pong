using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSprites : MonoBehaviour 
{
	public GameObject paddle;
	public GameObject healthBar;
	public GameObject smallCharacter;
	public GameObject bigCharacter;

	public Character character;

	void Start () 
	{
		paddle = transform.Find("Paddle").Find ("Paddle Sprite").gameObject;
		healthBar = transform.Find("Health Bar").Find ("Health Sprite").gameObject;
		//smallCharacter = transform.Find ("Character Sprite").gameObject;
		//bigCharacter = transform.Find ("Big Character Sprite").gameObject;
		character = transform.GetComponent<Character>();


		paddle.GetComponent<SpriteRenderer> ().sprite = character.paddleHead;
		healthBar.GetComponent<SpriteRenderer> ().sprite = character.healthHead;
		//smallCharacter.GetComponent<SpriteRenderer> ().sprite = character.smallBody;
		//bigCharacter.GetComponent<SpriteRenderer> ().sprite = character.bigBody;

		if (transform.parent != null && transform.parent.name  == "Player Party") 
		{
			paddle.GetComponent<SpriteRenderer> ().flipY = true;
			paddle.transform.localPosition = new Vector3 (0, 0.51f, 0);

			healthBar.GetComponent<SpriteRenderer> ().flipY = true;
			healthBar.transform.localPosition = new Vector3 (0, 0.121f, 0);
		}

	}
}

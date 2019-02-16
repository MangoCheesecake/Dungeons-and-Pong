using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour 
{
	public List<GameObject> enemyParty = new List<GameObject>();
	public List<GameObject> playerParty = new List<GameObject>();
	public Transform[] enemyTrans = new Transform[6];
	public Transform[] playerTrans = new Transform[6];

	public GameObject characterPrefab;

	public BattleHandler battleHandler;
	public bool player;
	public bool enemy;

	void Start () 
	{
		battleHandler = FindObjectOfType<BattleHandler>();
		enemyParty = battleHandler.enemyParty;
		playerParty = battleHandler.playerParty;
			
		int counter = 0;
		int position = 0;
		float offset = 0;

		if (player) 
		{
			if (playerParty.Count == 1 || playerParty.Count == 2) 
			{
				position = 2;
			} 
			else if (playerParty.Count == 3 || playerParty.Count == 4) 
			{
				position = 1;
			}

			if (playerParty.Count % 2 != 0) 
			{
				offset = 1.1602f;
			}


			foreach (GameObject character in playerParty)
			{
				Vector2 pos = new Vector2 (playerTrans [position].position.x + offset, playerTrans [position].position.y);
				Instantiate(characterPrefab, pos, playerTrans[position].rotation).transform.parent = transform;
				transform.GetChild (counter).GetComponent<Character> ().LoadCharacter (character.GetComponent<Character> ());
				counter++;
				position++;
			}
		}
			
		if (enemy) 
		{
			if (enemyParty.Count == 1 || enemyParty.Count == 2) 
			{
				position = 2;
			} 
			else if (enemyParty.Count == 3 || enemyParty.Count == 4) 
			{
				position = 1;
			}

			if (enemyParty.Count % 2 != 0) 
			{
				offset = 1.1602f;
			}
			foreach (GameObject character in enemyParty)
			{
				Vector2 pos = new Vector2 (enemyTrans [position].position.x + offset, enemyTrans [position].position.y);
				Instantiate(characterPrefab, pos, Quaternion.identity).transform.parent = transform;
				transform.GetChild (counter).GetComponent<Character> ().LoadCharacter (character.GetComponent<Character> ());
				counter++;
				position++;
			}
		}

	}
		
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public Sprite healthHead;
	public Sprite paddleHead;
	public Sprite smallBody;
	public Sprite bigBody;

	//used for identification
	public int id;
	public string characterName;
	public bool isAlive = true;

	//stats of character
	public float vitality;
	public float mobility;
	public float mana_recovery;
	public float mana_capacity;
	public float size;

	public void LoadCharacter(Character characterValues)
	{

	
		healthHead = characterValues.healthHead;
		paddleHead = characterValues.paddleHead;
		smallBody = characterValues.smallBody;
		bigBody = characterValues.bigBody;
	
		id = characterValues.id;
		characterName = characterValues.characterName;
		isAlive = characterValues.isAlive;

		vitality = characterValues.vitality;
		mobility = characterValues.mobility;
		mana_recovery = characterValues.mana_recovery;
		mana_capacity = characterValues.mana_capacity;
		size = characterValues.size;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfoSender : MonoBehaviour 
{
	public BattleHandler battleHandler;

	void Start()
	{
		battleHandler = GameObject.Find("Battle Handler").GetComponent<BattleHandler>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Character") 
		{
			battleHandler.addEnemy (other.gameObject);
			other.transform.parent = GameObject.Find ("Battle Handler").transform;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Character") 
		{
			if (GameObject.Find ("Character Holder") != null) 
			{
				battleHandler.removeEnemy (other.gameObject);
				other.transform.parent = GameObject.Find ("Character Holder").transform;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInitiator : MonoBehaviour 
{
	public BattleHandler battleHandler;
	public bool wait = false;

	void Start()
	{
		battleHandler = GameObject.Find("Battle Handler").GetComponent<BattleHandler>();
		StartCoroutine(waitOneSec ());
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "Character" && wait == true ) 
		{
			StartCoroutine(battleHandler.startBattle ());
		}
	}

	public IEnumerator waitOneSec()
	{
		yield return new WaitForSeconds(1);
		wait = true;
	}
}



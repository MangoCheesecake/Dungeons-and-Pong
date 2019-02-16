using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleHandler : MonoBehaviour 
{
	//all map characters
	//public List<GameObject> characters = new List<GameObject>();
	public GameObject[] characters; 
	//enemy party
	public List<GameObject> enemyParty = new List<GameObject>();
	//player party
	public List<GameObject> playerParty = new List<GameObject>();

	//public Camera cam;
	public GameObject cam;


	//ints to delete
	public List<int> deleteIds = new List<int>();
	//persistency bool
	public static bool battleHandlerExists;

	//scripts
	public BattleSaveLoad battleSaveLoad;
	public ScreenFader sf;

	void Start () 
	{
		battleSaveLoad = FindObjectOfType<BattleSaveLoad> ();
		characters = GameObject.FindGameObjectsWithTag("Character");
		sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
		//cam = GameObject.Find ("Main Camera").GetComponent<Camera>();
		cam = GameObject.Find("Main Camera");
		DontDestroyOnLoad(transform.gameObject);

		if (!battleHandlerExists) 
		{
			battleHandlerExists = true;
		} 
		else 
		{
			Destroy (gameObject);
		}
	}
		
	public void addEnemy(GameObject enemy)
	{
		if (enemyParty.Count < 6) 
		{
			enemyParty.Add (enemy);
		}
	}

	public void removeEnemy(GameObject enemy)
	{
		enemyParty.Remove (enemy);
	}


	public void addParty(GameObject party)
	{
		if (playerParty.Count < 6) 
		{
			playerParty.Add (party);
		}
	}
		
	public void removeParty(GameObject party)
	{
		playerParty.Remove (party);
	}

	public void clearEnemy()
	{
		enemyParty.Clear ();
	}

	public void clearParty()
	{
		playerParty.Clear ();
	}

	public IEnumerator startBattle()
	{
		yield return StartCoroutine (sf.FadeToBlack ());

		/*
		cam.GetComponent<CameraController>().enabled = false;
		cam.transform.position = new Vector3(0,0,-10);
		cam.orthographicSize = 4.5f;
		*/

		cam.SetActive (false);

		battleSaveLoad = FindObjectOfType<BattleSaveLoad> ();
		battleSaveLoad.SaveData ();
		//stop all characters from moving
		FindObjectOfType<PlayerController> ().canMove = false;
		foreach (GameObject character in GameObject.FindGameObjectsWithTag ("Character")) 
		{
			character.GetComponent<MonsterController> ().canMove = false;
		}

		SceneManager.LoadScene ("Pong Battle", LoadSceneMode.Single);


		//hide all characters
		foreach (GameObject enemy in enemyParty) 
		{
			enemy.transform.position = new Vector2(-5,-10);
		}

		GameObject.Find ("Player").transform.position = new Vector2 (-10, -10);

		foreach (GameObject ally in playerParty) 
		{
			ally.transform.position = new Vector2(-5,-10);
		}

		yield return StartCoroutine (sf.FadeToClear ());
	}

	public IEnumerator endBattle()
	{
		yield return StartCoroutine (sf.FadeToBlack ());

		/*
		cam.GetComponent<CameraController>().enabled = true;
		cam.orthographicSize = 2f;
		*/

		cam.SetActive (true);

		deleteParty ();
		battleSaveLoad = FindObjectOfType<BattleSaveLoad> ();
	
		battleSaveLoad.LoadData ();
		StartCoroutine(deleteEnemies());

		//allow all to move
		FindObjectOfType<PlayerController> ().canMove = true;

		yield return StartCoroutine (sf.FadeToClear ());
	}

	public void deleteParty()
	{
		//gets ids of chracters to delete
		foreach (GameObject enemy in enemyParty) 
		{
			deleteIds.Add (enemy.GetComponent<Character> ().id);
		}

		//deletes enemy party
		//clear enemy party list
		foreach (Transform child in transform) 
		{
			Destroy(child.gameObject);
		}

		clearEnemy ();
	}

	public void delete()
	{
		//delete enemy from map
		characters = GameObject.FindGameObjectsWithTag("Character");
		foreach (GameObject character in GameObject.FindGameObjectsWithTag("Character")) 
		{
			if(deleteIds.Contains(character.GetComponent<Character>().id))
			{
				Destroy(character);
			}
		}

	}
		
	IEnumerator deleteEnemies()
	{
		yield return null;
		delete ();
	}

	public void endBattleButton()
	{
		StartCoroutine(endBattle());
	}
}

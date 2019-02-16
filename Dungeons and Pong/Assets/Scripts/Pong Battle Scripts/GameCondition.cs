using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCondition : MonoBehaviour 
{

	public GameObject enemyParty;
	public GameObject playerParty;
	public GameObject battleOverText;
	public GameObject button;

	public BattleHandler battleHandler;

	void Start()
	{
		battleHandler = GameObject.Find ("Battle Handler").GetComponent<BattleHandler> ();
		enemyParty = GameObject.Find ("Enemy Party");
		playerParty = GameObject.Find ("Player Party");
	}

	public void characterDied()
	{
		StartCoroutine(CheckEnemy ());
		StartCoroutine(CheckPlayer ());
	}

	public IEnumerator CheckEnemy()
	{
		Character state;
		bool allDead = true;

		foreach (Transform character in enemyParty.transform)
		{
			state = character.GetComponent<Character> ();
			if (state.isAlive == true) 
			{
				allDead = false;
			}
		}

		if (allDead) 
		{
			battleOverText.SetActive (true);
			battleOverText.GetComponent<Text>().text = "Victory!";
			foreach(GameObject ball in GameObject.FindGameObjectsWithTag ("Ball"))
			{
				ball.GetComponent<Ball> ().enabled = false;
				ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
			yield return new WaitForSeconds(2.0f);
			button.SetActive (true);
			button.GetComponentInChildren<Text>().text = "Continue";

			button.GetComponent<Button> ().onClick.AddListener(battleHandler.endBattleButton);
		}
	}


	public IEnumerator CheckPlayer()
	{
		Character state;
		bool allDead = true;

		foreach (Transform character in playerParty.transform)
		{
			state = character.GetComponent<Character> ();
			if (state.isAlive == true) 
			{
				allDead = false;
			}
		}

		if (allDead) 
		{
			battleOverText.SetActive (true);
			battleOverText.GetComponent<Text>().text = "Defeat!";
			foreach(GameObject ball in GameObject.FindGameObjectsWithTag ("Ball"))
			{
				ball.GetComponent<Ball> ().enabled = false;
				ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
			yield return new WaitForSeconds(2.0f);
			button.SetActive (true);
			button.GetComponentInChildren<Text>().text = "Quit";
			button.GetComponent<Button> ().onClick.AddListener(titleScreen);
		}
	}

	public void titleScreen()
	{
		SceneManager.LoadScene ("Title_Screen", LoadSceneMode.Single);
		CameraController.cameraExists = false;
		Destroy(battleHandler.cam);
		PlayerController.playerExists = false;
		Destroy (GameObject.Find ("Player"));
		BattleHandler.battleHandlerExists = false;
		Destroy (GameObject.Find ("Battle Handler"));
		ScreenFader.hudExists = false;
		Destroy (GameObject.Find ("HUD")); 
	}


	public IEnumerator battleOver()
	{
		Time.timeScale = 0.5f;
		//destory indestructable objects
		yield return new WaitForSeconds(1.0f);
		battleOverText.SetActive (true);
		battleOverText.GetComponent<Text>().text = "Defeat!";
		foreach(GameObject ball in GameObject.FindGameObjectsWithTag ("Ball"))
		{
			ball.GetComponent<Ball> ().enabled = false;
			ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
		Time.timeScale = 0.25f;
		yield return new WaitForSeconds(0.5f);
		Time.timeScale = 0.1f;
		foreach(GameObject ball in GameObject.FindGameObjectsWithTag ("Ball"))
		{
			ball.GetComponent<Ball> ().enabled = false;
			ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}

		button.SetActive (true);
		button.GetComponentInChildren<Text>().text = "Quit";
		button.GetComponent<Button> ().onClick.AddListener(titleScreen);
	}
}

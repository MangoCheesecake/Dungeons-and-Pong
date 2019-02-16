using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour 
{
	public string currentMap;
	public string mapToLoad;
	public string direction;

	private GameObject theMap;
	private PlayerController thePlayer;
	private CameraController theCamera;
	private ScreenFader sf;

	void Start()
	{
		theMap = GameObject.FindGameObjectWithTag ("Map");
		thePlayer = FindObjectOfType<PlayerController> ();
		theCamera = FindObjectOfType<CameraController>();

		sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
	}

	IEnumerator OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") 
		{
			//stops all characters from moving
			stopCharacters();

			//fade screen
			yield return StartCoroutine (sf.FadeToBlack ());

			//destroy odd map and scene
			SceneManager.UnloadSceneAsync(currentMap);
			Destroy (theMap);

			//load new map
			SceneManager.LoadScene(mapToLoad, LoadSceneMode.Additive);

			//set postion based on direction player moves
			switch (direction) 
			{
			case "up":
				thePlayer.transform.position = new Vector3 (thePlayer.transform.position.x, -7.85f, 0);
				break;
			case "down":
				thePlayer.transform.position = new Vector3 (thePlayer.transform.position.x, 0.05f, 0);
				break;
			case "left":
				thePlayer.transform.position = new Vector3 (7.95f, thePlayer.transform.position.y, 0);
				break;
			case "right":
				thePlayer.transform.position = new Vector3 (0.05f, thePlayer.transform.position.y, 0);
				break;
			}

			//moves camera to player position
			theCamera.transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y, -10);

			//lets all characters move again
			moveCharacters();

			yield return StartCoroutine (sf.FadeToClear ());
		}
	}

	public void stopCharacters()
	{
		//stops player from moving
		thePlayer.GetComponent<PlayerController>().canMove = false;

		//stop all other characters from moving
		foreach (GameObject character in GameObject.FindGameObjectsWithTag ("Character")) 
		{
			character.GetComponent<MonsterController> ().canMove = false;
		}

	}

	public void moveCharacters()
	{
		//allows player to move
		thePlayer.GetComponent<PlayerController>().canMove = true;

		//allows all other characters to move
		foreach (GameObject character in GameObject.FindGameObjectsWithTag ("Character")) 
		{
			character.GetComponent<MonsterController> ().canMove = true;
		}
	}
}

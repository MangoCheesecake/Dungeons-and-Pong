using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class BattleSaveLoad : MonoBehaviour
{
	public GameObject player;
	public GameObject cam;
	public GameObject[] characters;

	private Scene scene;
	private int charactersSize;

	public BattleInitiator battleInitiator;

	void Start()
	{
		player = GameObject.Find ("Player");
		cam = GameObject.Find ("Main Camera");
		characters = GameObject.FindGameObjectsWithTag ("Character");
		charactersSize = characters.Length;
		battleInitiator = FindObjectOfType<BattleInitiator> ();
	}

	public void SaveData()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = new FileStream (Application.persistentDataPath + "/map.sav", FileMode.Create);

		PlayerData pd = new PlayerData ();
		CharacterHolder ch = new CharacterHolder();

		pd.posx = player.transform.position.x;
		pd.posy = player.transform.position.y;
		pd.posz = player.transform.position.z;

		characters = GameObject.FindGameObjectsWithTag ("Character");
		charactersSize = characters.Length;

		ch.xyz = new float[30];
		ch.name = new string[10];

		for(int i = 0; i < charactersSize; i++)
		{
			foreach (GameObject character in characters) 
			{
				Character stats = character.GetComponent<Character> ();

				if (stats.id == i) 
				{
					Transform pos = character.GetComponent<Transform> ();
					ch.name [i] = stats.name;
					ch.xyz [i * 3] = character.transform.position.x;
					ch.xyz[i * 3 + 1] = pos.position.y;
					ch.xyz[i * 3 + 2] = pos.position.z;
				}
			}
		}

		scene = SceneManager.GetActiveScene ();
		pd.sceneName = scene.name;

		bf.Serialize (stream, pd);
		bf.Serialize (stream, ch);
		stream.Close ();
	}

	public void LoadData()
	{
		if (File.Exists (Application.persistentDataPath + "/map.sav")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + "/map.sav", FileMode.Open);
		
			PlayerData pd = bf.Deserialize (stream) as PlayerData;
			CharacterHolder ch = bf.Deserialize (stream) as CharacterHolder;

			stream.Close ();

			SceneManager.LoadScene (pd.sceneName, LoadSceneMode.Single);

			characters = GameObject.FindGameObjectsWithTag ("Character");
			charactersSize = characters.Length;

			player = GameObject.Find ("Player");
			//player.GetComponent<LoadPosition>().LoadDataState(pd.posx, pd.posy, pd.posz);
			player.transform.position = new Vector3 (pd.posx, pd.posy, pd.posz);


			StartCoroutine (setPositions (ch));

			battleInitiator.wait = false;
			StartCoroutine(battleInitiator.waitOneSec ());

		}
	}
		
	public void setPosition(CharacterHolder ch)
	{
		characters = GameObject.FindGameObjectsWithTag ("Character");
		charactersSize = characters.Length;

		for(int i = 0; i < charactersSize; i++)
		{
			foreach (GameObject character in characters) 
			{
				Character stats = character.GetComponent<Character> ();

				if (stats.id == i) 
				{
					//character.GetComponent<LoadPosition>().LoadDataState(ch.xyz[i * 3 ], ch.xyz[i * 3 + 1], ch.xyz[i * 3 + 2]);
					character.transform.position = new Vector3 (ch.xyz[i * 3 ], ch.xyz[i * 3 + 1], ch.xyz[i * 3 + 2]);
				}
			}

		}
	}

	IEnumerator setPositions(CharacterHolder ch)
	{
		yield return null;
		setPosition (ch);
	}
}
	
[Serializable]
public class PlayerData
{
	public string sceneName;

	public float posx;
	public float posy;
	public float posz;
}
	
[Serializable]
public class CharacterHolder
{
	public float[] xyz;
	public int[] ids;
	public string[] name;
}


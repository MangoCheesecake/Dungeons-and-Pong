using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour 
{
	public AudioSource audioSource;

	public static bool musicPlayerExists;

	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = GameObject.Find ("Music").GetComponent<AudioSource> ().clip;
		audioSource.Play ();

		DontDestroyOnLoad(transform.gameObject);

		if (!musicPlayerExists) 
		{
			musicPlayerExists = true;
		} 
		else 
		{
			Destroy(gameObject);
		}
	}
	
	public void updateMusic()
	{
		if (audioSource.clip != GameObject.Find ("Music").GetComponent<AudioSource> ().clip) 
		{
			audioSource.clip = GameObject.Find ("Music").GetComponent<AudioSource> ().clip;
			audioSource.Play ();
		}
	}
}

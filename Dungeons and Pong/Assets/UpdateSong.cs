using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSong : MonoBehaviour 
{
	public MusicPlayer musicPlayer;

	void Start () 
	{
		musicPlayer = GameObject.Find ("Music Player").GetComponent<MusicPlayer>();
		musicPlayer.updateMusic ();
	}

}

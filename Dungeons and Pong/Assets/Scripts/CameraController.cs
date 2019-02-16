using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	//camera that follows the target smoothly
	//default set on the player
	public Transform target;

	private Camera mycam;
	public static bool cameraExists;

	void Start () 
	{
		mycam = GetComponent<Camera> ();
		DontDestroyOnLoad(transform.gameObject);

		//if camera already exists, destory new one made
		if (!cameraExists) 
		{
			cameraExists = true;
		} 
		else 
		{
			Destroy (gameObject);
		}
	}

	void FixedUpdate () 
	{
		mycam.orthographicSize = (Screen.height / 100f / 8f );

		//if target is not null, move camera to target position -10 in the z axis
		//lerp makes the camera move smoothly
		if (target) 
		{
			transform.position = Vector3.Lerp (transform.position, target.position, 0.1f) + new Vector3 (0, 0, -10);
		}
	}
}

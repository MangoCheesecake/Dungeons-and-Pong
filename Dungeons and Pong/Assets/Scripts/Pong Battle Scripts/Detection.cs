using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour 
{
	AIPaddleController aiPad;
	public List<GameObject> objects = new List<GameObject>();

	void Start ()
	{
		aiPad = transform.parent.Find("Paddle").GetComponent<AIPaddleController>();

	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.tag == "Ball" && !objects.Contains(other.gameObject) && aiPad != null) 
		{
			objects.Add (other.gameObject);
			aiPad.ball = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (objects.Contains(other.gameObject)) 
		{
			objects.Remove (other.gameObject);
			/*
			if (objects [0] != null) 
			{
				aiPad.ball = objects[0];
			}
			*/
		}
	}
}

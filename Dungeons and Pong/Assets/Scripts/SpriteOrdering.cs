using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrdering : MonoBehaviour 
{
	//dynamically sets the sprite order of a sprite based on the sprite's y axis location
	void FixedUpdate () 
	{
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
	}
}

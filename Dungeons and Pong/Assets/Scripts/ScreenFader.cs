﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour 
{
	Animator anim;
	bool isFading = false;

	public static bool hudExists;


	void Start () 
	{
		anim = GetComponent<Animator> ();

		DontDestroyOnLoad(transform.parent.gameObject);

		//if camera already exists, destory new one made
		if (!hudExists) 
		{
			hudExists = true;
		} 
		else 
		{
			Destroy (transform.parent.gameObject);
		}
	}

	public IEnumerator FadeToClear()
	{
		isFading = true;
		anim.SetTrigger ("FadeIn");

		while (isFading) 
		{
			yield return null;
		}
	}

	public IEnumerator FadeToBlack()
	{
		isFading = true;
		anim.SetTrigger ("FadeOut");

		while (isFading) 
		{
			yield return null;
		}
	}

	void AnimationComplete()
	{
		isFading = false;
	}
}

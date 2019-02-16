using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAboutScreen : MonoBehaviour {
	private AssetBundle myLoadedAssetBundle;
	private string[] scenePaths;

	void start(){
		myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/scenes");
		scenePaths = myLoadedAssetBundle.GetAllScenePaths();
	}

	void OnMouseDown(){
		SceneManager.LoadScene ("About Section");
	}
}

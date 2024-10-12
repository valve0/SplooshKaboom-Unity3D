using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnInput : MonoBehaviour {

	public GameObject LevelLoader;
	private LevelLoader levelLoader;

	void Start() 
	{ 
		levelLoader = LevelLoader.GetComponent<LevelLoader>();
	}

	void Update () {
		if (Input.GetAxis("Submit") == 1)
		{
			levelLoader.LoadNextLevel();
		}
	}
}

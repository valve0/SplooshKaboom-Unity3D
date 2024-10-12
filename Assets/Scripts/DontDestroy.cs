﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {

	// make this static so it's visible across all instances
	public static DontDestroy instance = null;

	public string DeleteOnSceneChange;

	private Scene currentScene;

	// singleton pattern; make sure only one of these exists at one time, else we will
	// get an additional set of sounds with every scene reload, layering on the music
	// track indefinitely
	void Awake() {
	
		Scene newScene = SceneManager.GetActiveScene();
        //Debug.Log(newScene.name);
        if (DeleteOnSceneChange == newScene.ToString())
		{
            Destroy(gameObject);
        }
		else
		{
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

	}

	// Use this for initialization
	void Start () {
		
	}
}

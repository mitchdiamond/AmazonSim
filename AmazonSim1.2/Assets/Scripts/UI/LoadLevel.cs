﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

	public string levelName;
	
	public GameObject gameStateManager;

	void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void SetScene(string _levelName)
	{
		levelName = _levelName;
	}

	public void Load()
	{
		gameStateManager.GetComponent<Animator>().SetTrigger("LoadLevel");
		SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
	}

	public void OnSceneLoaded(Scene _scene, LoadSceneMode _loaded)
	{
		gameStateManager.GetComponent<Animator>().SetTrigger("StartGame");

	}

	public void Unload()
	{
		SceneManager.UnloadSceneAsync(levelName);
	}
}
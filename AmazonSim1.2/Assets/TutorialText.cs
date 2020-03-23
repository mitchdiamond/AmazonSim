using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.Rendering;


public class TutorialText : MonoBehaviour
{
	public string fileName;
	public List<string> tutorialLines;
	private StreamReader reader;

	private void Start()
	{
		string filePath = Application.dataPath + '/' + fileName;
		Debug.Log(filePath);
		reader = new StreamReader(filePath);
		while (!reader.EndOfStream)
		{
			string tempLine = reader.ReadLine();
			Debug.Log(tempLine);
			if (tempLine.ToLower() == "tutorial")
			{
				Debug.Log("Tutorial Found");
				CreateTutorialArray();
			}


		}

		foreach (string line in tutorialLines)
		{
			Debug.Log(line);
		}

		reader.Close();
		
	}

	void CreateTutorialArray()
	{
		while (!reader.EndOfStream)
		{
			string tempLine = reader.ReadLine();
			if (tempLine != "end flag")
			{
				tutorialLines.Add(tempLine);
			}
			else
			{
				return;
			}
		}
	}
}

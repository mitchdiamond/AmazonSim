using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	//public List<string> tutorialStrings;
	
	public string fileName;
	public List<string> lines;
	
	public Text text;
	public int textNum = 0;

	public Button nextButton;
	public Button okayButton;

	void Start()
	{
		string filePath = Application.dataPath + '/' + fileName;
		Debug.Log(filePath);
		StreamReader reader = new StreamReader(filePath);
		while (!reader.EndOfStream)
		{
			lines.Add(reader.ReadLine());
		}
		

		reader.Close();
		if(lines.Capacity!=0)
			text.text = lines[0];
	}

	public void ChangeText()
	{
		textNum++;
		text.text = lines[textNum];

		if(textNum == lines.Count - 1)
		{
			nextButton.gameObject.SetActive(false);
			okayButton.gameObject.SetActive(true);
		}
	}

}

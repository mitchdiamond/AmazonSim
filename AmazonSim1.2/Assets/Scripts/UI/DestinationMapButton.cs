using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestinationMapButton : MonoBehaviour {

	public DestinationButtonManager destinationButtonManager;

	public SO_Destination sO_Destination;
	public SO_Package sO_Package;
	public Text timerText;

	public DestinationButton destinationButton;

	public void Start()
	{
		gameObject.SetActive(false);
	}

	public void Display(int existTime)
	{
		timerText.text = existTime.ToString();
	}

	public void SetLastClicked()
	{
		destinationButtonManager.lastButtonClicked = destinationButton;
	}
}
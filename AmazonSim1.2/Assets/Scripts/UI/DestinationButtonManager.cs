using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationButtonManager : MonoBehaviour {

	public List<DestinationButton> destinationButtons;

	public DestinationButton lastButtonClicked;

	public static DestinationButtonManager instance;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(this);
		}
	}

	public void OnButtonClicked(int destinationButtonNumber)
	{
		foreach (DestinationButton button in destinationButtons)
		{
			if(button.buttonNum != destinationButtonNumber && button != null)
			{
				button.GetComponent<OnClickColors>().OnOtherButtonClicked();
			}

			else
			{
				lastButtonClicked = button;
			}
		}
	}

	public void DeleteLastClicked()
	{	
		if(lastButtonClicked != null)
		{
			destinationButtons.Remove(lastButtonClicked);
			Destroy(lastButtonClicked.gameObject);
		}
	}
}

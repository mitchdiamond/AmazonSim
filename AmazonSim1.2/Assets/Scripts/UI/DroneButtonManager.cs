using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneButtonManager : MonoBehaviour {

	public List<DroneButton> droneButtons;

	public void OnButtonClicked(int droneButtonNumber)
	{
		foreach (DroneButton button in droneButtons)
		{
			print(button.buttonNumber);
			if(button.buttonNumber != droneButtonNumber)
			{
				button.GetComponent<OnClickColors>().OnOtherButtonClicked();
			}
		}
	}

	public void OnDispatch()
	{
		foreach (DroneButton button in droneButtons)
		{
			button.GetComponent<OnClickColors>().OnOtherButtonClicked();
		}
	}
}

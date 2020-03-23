using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroneStatusButtonManager : MonoBehaviour {

	public static DroneStatusButtonManager instance;
	public GameObject statusButtonPrefab;

	public List<GameObject> droneStatusButtons;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(this);
		}
	}

	public GameObject CreateButton(RenderTexture rt)
	{
		GameObject newButton = Instantiate(statusButtonPrefab);
		newButton.transform.SetParent(transform);
		newButton.transform.localScale = new Vector3(1, 1, 1);
		newButton.GetComponent<DroneStatusButton>().rt = rt;
		droneStatusButtons.Add(newButton);
		return newButton;
	}

	public void DeleteButton(GameObject button)
	{
		droneStatusButtons.Remove(button);
		Destroy(button);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneStatusPopupManager : MonoBehaviour {

	//TODO: Make this class dynamic (rendertexture changes based on which button is being hovered over)
	public GameObject statusPopup;

	public static DroneStatusPopupManager instance;

	public RawImage ri;

	// Use this for initialization
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

	public void SetActive(RenderTexture tex)
    {
		statusPopup.SetActive(true);
		ri.texture = tex;
		
    }

	public void SetInactive()
	{
		statusPopup.SetActive(false);
		//ri.texture = null;
	}



}

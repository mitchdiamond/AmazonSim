using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCamFollow : MonoBehaviour {

	public Camera mainCamera;
	//public Transform obj;

	void Start()
	{
		mainCamera = GameObject.FindObjectOfType<Camera>();
	}

	public void SetDroneToFollow()
	{
		mainCamera.GetComponent<CameraFollow>().objToFollow = transform;
		mainCamera.GetComponent<CameraFollow>().StartFollow();
	}

}

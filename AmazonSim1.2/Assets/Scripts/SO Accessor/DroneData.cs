using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneData : MonoBehaviour {

	public SO_Drone sO_Drone;
	public SO_Package sO_Package;
	public SO_Destination sO_Destination;
	public Transform droneMesh;

	public Camera dronePOVCam;
	
	public bool isDispatched = false;
	public bool atDest = false;
	public bool hasPackage = false;

	public GameObject droneFuelUI;
	public GameObject droneButton;
	public GameObject droneStatusButton;

	public Transform destAir;
	public Transform destGround;

	public void StartDeliveryTimer()
	{
		StartCoroutine(DeliveryTimer());
	}

	IEnumerator DeliveryTimer()
	{
		float deliveryTime = 0;

		while(isDispatched)
		{
			yield return new WaitForSeconds(0.1f);
			deliveryTime += 0.1f;
		}

		GameData.instance.IncreaseDeliveryTime(deliveryTime);
	}

}
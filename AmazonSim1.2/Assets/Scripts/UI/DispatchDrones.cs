using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DispatchDrones : MonoBehaviour {

	public UnityEvent cantDispatchEvent;
	public UnityEvent canDispatchEvent;
	public UnityEvent noDroneEvent;
	public UnityEvent noDestEvent;

	private DroneData droneData;

	public void Dispatch()
	{	
		if(DroneManager.instance.currentDrone != null)
		{
			droneData = DroneManager.instance.currentDrone.GetComponent<DroneData>();
		}
		
		if(!droneData.isDispatched && DroneManager.instance.currentDrone != null && DroneManager.instance.currentDestination != null)
		{
			droneData.destAir = DroneManager.instance.currentDestination.GetComponent<DestData>().dropspotAir;
			droneData.destGround = DroneManager.instance.currentDestination.GetComponent<DestData>().dropspotGround;
			droneData.sO_Package = DroneManager.instance.currentPackage;
			droneData.sO_Destination = DroneManager.instance.currentsO_Destination;
			droneData.droneMesh.GetComponent<DroneVertMovement>().Ascend();
			droneData.sO_Package.isEnRoute = true;
			droneData.hasPackage = true;
			droneData.isDispatched = true;
			DroneManager.instance.currentDrone.GetComponent<DroneAlertSystem>().OnLeftWarehouse();
			droneData.StartDeliveryTimer();
			droneData.GetComponentInChildren<DroneCrash>().StartCalcCrash();
			droneData.droneButton.SetActive(false);

			DestinationButtonManager.instance.lastButtonClicked.mapButton.SetActive(false);

			canDispatchEvent.Invoke();
		}

		else if (DroneManager.instance.currentDrone == null)
		{
			noDroneEvent.Invoke();
		}

		else if (DroneManager.instance.currentDestination == null)
		{
			noDestEvent.Invoke();
		}

		else
		{
			cantDispatchEvent.Invoke();
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PurchaseDrone : MonoBehaviour {

	public DroneCreator droneCreator;

	public SO_Drone.DroneType droneType;

	public int droneCost = 200;

	public UnityEvent notEnoughPointsEvent;
	public UnityEvent tooManyDronesEvent;

	public void OnPurchase()
	{
		if(PointManager.instance.currentPoints >= droneCost)
		{
			if (DroneManager.instance.drones.Count < DroneManager.instance.maxDrones)
			{
				droneCreator.CreateDrone();
				PointManager.instance.ChangePoints(-droneCost);
				GameData.instance.IncreaseDrones(1);
			}
			else
			{
				tooManyDronesEvent.Invoke();
			}
		}
		else
		{
			notEnoughPointsEvent.Invoke();
		}
	}

}

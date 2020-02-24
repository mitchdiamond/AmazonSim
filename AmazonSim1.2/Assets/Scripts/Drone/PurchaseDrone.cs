using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PurchaseDrone : MonoBehaviour {

	public DroneCreator droneCreator;
	//public PointManager pointManager;

	public SO_Drone.DroneType droneType;

	public int droneCost = 200;
	private int droneCostMultiplier = 2;
	//public GameData gameData;

	public UnityEvent notEnoughPointsEvent;

	public void OnPurchase()
	{
		if(PointManager.instance.currentPoints >= droneCost)
		{
			droneCreator.CreateDrone();
			PointManager.instance.ChangePoints(-droneCost);
			droneCost *= droneCostMultiplier;
			droneCostMultiplier++;
			GameData.instance.IncreaseDrones(1);
		}
		else
		{
			notEnoughPointsEvent.Invoke();
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneFuel : MonoBehaviour {

	private DisplayUIImageFill displayUIImageFill;
	private DroneData droneData;
	private NavMeshAgent agent;

	private DroneHorizMovement droneHorizMovement;


	private bool useFuelRunning = false;

	void Start()
	{
		displayUIImageFill = GetComponent<DisplayUIImageFill>();
		droneData = GetComponent<DroneData>();
		agent = GetComponent<NavMeshAgent>();
		droneHorizMovement = GetComponent<DroneHorizMovement>();

		displayUIImageFill.Display(droneData.sO_Drone.currentFuelLevel/100);
	}



	public IEnumerator UseFuel(float pgkWgt)
	{
		if(!useFuelRunning)
		{
			useFuelRunning = true;

			while(!droneData.atDest)
			{
				droneData.sO_Drone.currentFuelLevel -= (pgkWgt/10);

				if (droneData.sO_Drone.currentFuelLevel <= 0)
				{
					droneData.sO_Drone.currentFuelLevel = 0;
					droneHorizMovement.StopDrone();					
				}

				displayUIImageFill.Display(droneData.sO_Drone.currentFuelLevel/100);
				//print(droneData.sO_Drone.currentFuelLevel);
				yield return new WaitForSeconds(.1f);
			}

			useFuelRunning = false;
		}
	}
	public IEnumerator Refuel()
	{
		while (droneData.sO_Drone.currentFuelLevel < 100)
		{
			droneData.sO_Drone.currentFuelLevel++;
			displayUIImageFill.Display(droneData.sO_Drone.currentFuelLevel/100);
			yield return new WaitForSeconds(1);
		}

		if(droneData.sO_Drone.currentFuelLevel > 100)
		{
			droneData.sO_Drone.currentFuelLevel = 100;
		}
	}
}

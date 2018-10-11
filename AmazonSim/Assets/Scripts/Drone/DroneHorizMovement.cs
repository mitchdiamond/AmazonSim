﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneHorizMovement : MonoBehaviour {

	private NavMeshAgent agent;
	private DroneFuel droneFuel;
	private DroneData droneData;
	public DroneVertMovement droneVertMovement;

	public float agentSpeed = 3.5f;

	private bool checkForPathRunning = false;

	void Start()
	{
		droneFuel = GetComponent<DroneFuel>();
		droneData = GetComponent<DroneData>();
		agent = GetComponent<NavMeshAgent>();
	}

	public void MoveNavMesh(Transform dest)
	{
		print(dest);

		if(droneData.sO_Drone.currentFuelLevel > 0)
		{
			NavMeshPath path = new NavMeshPath();
			agent.CalculatePath(dest.position, path);

			agent.speed = agentSpeed;
			agent.isStopped = false;
			agent.path = path;

			droneData.inMotion = true;
			droneData.atDest = false;

			StartCoroutine(CheckForPath(false));
			StartCoroutine(droneFuel.UseFuel());
		}
	}

		IEnumerator CheckForPath(bool destIsWarehouse)
	{
		print("starting check for path");

		if(!checkForPathRunning)
		{
			checkForPathRunning = true;

			while(agent.remainingDistance > 0.3 && droneData.inMotion)
			{
				print (agent.remainingDistance);
				yield return null;
			}

			if (droneData.inMotion == false)
			{
				print("stopped");
			}

			else
			{
				droneData.atDest = true;
				droneData.inMotion = false;
				print("at destination");
				agent.isStopped = true;

				if(droneData.hasPackage)
				{
					droneVertMovement.Descend();
				}
				else
				{
					droneData.destGround = droneData.warehouseGround;
					droneVertMovement.Descend();
				}
			}
			checkForPathRunning = false;
		}
	}

	public void ReturnToWarehouse()
	{
		NavMeshPath path = new NavMeshPath();
		agent.CalculatePath(droneData.droneManager.warehousePads[droneData.sO_Drone.droneNumber].position, path);
		agent.speed = agentSpeed;
		agent.isStopped = false;
		agent.path = path;
		droneData.inMotion = true;
		droneData.atDest = false;
		StartCoroutine(CheckForPath(true));
		StartCoroutine(droneFuel.UseFuel());
	}

}

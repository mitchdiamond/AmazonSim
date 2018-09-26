﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneData : MonoBehaviour {

	public DroneManager droneManager;
	public SO_Drone sO_Drone;
	private NavMeshAgent agent;
    private bool inMotion;
	private bool atDest = false;

	public float agentSpeed = 3.5f;
    private float packageDropoffTime = 5;

	private bool useFuelRunning = false;

    void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	public void MoveNavMesh(Transform dest)
	{
		if(sO_Drone.currentFuelLevel > 0)
		{
			NavMeshPath path = new NavMeshPath();
			agent.CalculatePath(dest.position, path);
			agent.speed = agentSpeed;
			agent.isStopped = false;
			agent.path = path;
			inMotion = true;
			atDest = false;
			StartCoroutine(CheckForPath());
			StartCoroutine(UseFuel());
		}
	}

	IEnumerator CheckForPath()
	{
		while(agent.remainingDistance > 0.3 && inMotion)
		{
			yield return null;
		}

		if (inMotion == false)
		{
			print("stopped");
		}
		else
		{
			atDest = true;
			//inMotion = false;
			StartCoroutine(DropOffPackage());
		}
	}

	IEnumerator UseFuel()
	{
		if(!useFuelRunning)
		{
			useFuelRunning = true;

			while(inMotion && !atDest)
			{
				sO_Drone.currentFuelLevel--;

				if (sO_Drone.currentFuelLevel <= 0)
				{
					sO_Drone.currentFuelLevel = 0;
					agent.isStopped = true;
					agent.speed = 0;
					inMotion = false;
				}

				yield return new WaitForSeconds(1);
			}

			useFuelRunning = false;
		}
	}

	IEnumerator DropOffPackage()
	{
		yield return new WaitForSeconds(packageDropoffTime);

		NavMeshPath path = new NavMeshPath();
		agent.CalculatePath(droneManager.warehousePads[sO_Drone.droneNumber].position, path);
		agent.speed = agentSpeed;
		agent.isStopped = false;
		agent.path = path;
		inMotion = true;
		atDest = false;
		StartCoroutine(CheckForPath());
		StartCoroutine(UseFuel());
	}



}
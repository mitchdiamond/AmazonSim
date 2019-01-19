using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneHorizMovement : MonoBehaviour {

	private NavMeshAgent agent;
	private DroneData droneData;
	public DroneVertMovement droneVertMovement;

	public float agentSpeed = 3.5f;

	private bool checkForPathRunning = false;

	void Start()
	{
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

			//agent.speed = agentSpeed;
			//agent.isStopped = false;
			agent.path = path;

			droneData.inMotion = true;

			StartCoroutine(CheckForPath(false));
			
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
				//print (agent.remainingDistance);
				yield return null;
			}

			if (!droneData.inMotion)
			{
				print("stopped");
			}

			else
			{
				droneData.inMotion = false;
				print("at destination");
				agent.isStopped = true;

				if(droneData.hasPackage)
				{
					droneVertMovement.Descend();
				}
				else
				{
					droneData.destGround = droneData.droneManager.warehousePads[droneData.sO_Drone.droneNumber].GetComponent<DestData>().dropspotGround;
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
		//agent.isStopped = false;
		agent.path = path;
		droneData.inMotion = true;
		StartCoroutine(CheckForPath(true));
	}

	public void StopDrone()
	{
		print("stopping drone");
		agent.isStopped = true;
		//agent.speed = 0;
		//droneData.inMotion = false;
	}

	public void StartDrone()
	{
		print("starting drone");
		agent.isStopped = false;
		//agent.speed = agentSpeed;
		//droneData.inMotion = true;
	}

}

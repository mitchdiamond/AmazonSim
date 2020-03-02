using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class DroneVertMovement : MonoBehaviour {

	public float packageDropoffTime = 3;
	public float droneVertSpeed = 1f;
	public Transform destColl;

	private DroneData droneData;
	private DroneFuel droneFuel;
	private DroneHorizMovement droneHorizMovement;

	public UnityEvent packageDropoffEvent;	

	private bool canMoveVert;
	private bool moveVertRunning = false;
	public Transform droneAirPos;

	

	void Start() 
	{
		droneFuel = transform.parent.GetComponent<DroneFuel>();
		droneData = transform.parent.GetComponent<DroneData>();
		droneHorizMovement = transform.parent.GetComponent<DroneHorizMovement>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<DropspotData>() != null)
		{
			if(!other.GetComponent<DropspotData>().isWarehouse)
			{
				droneData.atDest = true;
				destColl = droneData.destAir;
				StartCoroutine(DropOffPackage());
			}
			else
			{
				droneData.atDest = true;
				droneData.isDispatched = false;
				transform.parent.GetComponent<DroneAlertSystem>().OnReturnedWareHouse();
				droneData.droneButton.SetActive(true);
				StartCoroutine(droneFuel.Refuel());
			}
		}
	}

	public void Descend()
	{
		if(!moveVertRunning)
		{
			StartCoroutine(MoveDroneVert(droneData.destGround, false));
		}
		
	}

	public void Ascend()
	{
		droneData.atDest = false;

		if(!moveVertRunning)
		{
			StartCoroutine(MoveDroneVert(droneAirPos, true));
		}
		

		if(droneData.sO_Package != null)
		{
			StartCoroutine(droneFuel.UseFuel(droneData.sO_Package.pkgWeight));
		}
		else
		{
			StartCoroutine(droneFuel.UseFuel(0));
		}
		
	}

	IEnumerator DropOffPackage()
	{
		yield return new WaitForSeconds(packageDropoffTime);
		packageDropoffEvent.Invoke();
		droneData.hasPackage = false;
		Ascend();
	}

	IEnumerator MoveDroneVert(Transform coll, bool isOrigPos)
	{
		moveVertRunning = true;
		float lerpFrac = 0;
		Vector3 pos;
		
		if(isOrigPos)
		{
			pos = transform.localPosition;
		}
		else
		{
			pos = transform.position;
		}

		while(lerpFrac < 1)
		{
			lerpFrac += droneVertSpeed * Time.deltaTime;
			
			if(isOrigPos)
			{
				transform.localPosition = Vector3.Lerp(pos, coll.localPosition, lerpFrac);
			}
			else
			{
				transform.position = Vector3.Lerp(pos, coll.position, lerpFrac);
			}

			yield return null;
		}

		if(droneData.hasPackage)
		{
			droneHorizMovement.MoveNavMesh(droneData.destAir);
		}
		else
		{
			droneHorizMovement.ReturnToWarehouse();
		}
		moveVertRunning = false;
	}
}

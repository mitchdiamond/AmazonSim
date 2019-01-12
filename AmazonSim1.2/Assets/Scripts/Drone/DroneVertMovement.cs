using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneVertMovement : MonoBehaviour {

	public float packageDropoffTime = 3;
	public float droneVertSpeed = 1f;
	public Transform destColl;

	private DroneData droneData;
	private DroneFuel droneFuel;
	private DroneHorizMovement droneHorizMovement;
	

	private bool canMoveVert;
	public Transform droneAirPos;

	void Start() 
	{
		droneFuel = transform.parent.GetComponent<DroneFuel>();
		droneData = transform.parent.GetComponent<DroneData>();
		droneHorizMovement = transform.parent.GetComponent<DroneHorizMovement>();
	}

	void OnTriggerEnter(Collider other)
	{
		print("entered collider");
		droneData.atDest = true;

		if(!other.GetComponent<DropspotData>().isWarehouse)
		{
			destColl = droneData.destAir;
			print("the destination is " + destColl);
			StartCoroutine(DropOffPackage());
		}
		else
		{
			StartCoroutine(droneFuel.Refuel());
		}
	}

	public void Descend()
	{
		StartCoroutine(MoveDroneVert(droneData.destGround, false));
	}

	public void Ascend()
	{
		droneData.atDest = false;
		print("ascending");
		print(droneAirPos);
		print(droneData.sO_Package.pkgWeight);
		StartCoroutine(MoveDroneVert(droneAirPos, true));
		StartCoroutine(droneFuel.UseFuel(droneData.sO_Package.pkgWeight));
	}

	IEnumerator DropOffPackage()
	{
		print("dropping off package");
		yield return new WaitForSeconds(packageDropoffTime);
		droneData.hasPackage = false;
		Ascend();
	}

	IEnumerator MoveDroneVert(Transform coll, bool isOrigPos)
	{
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
	}
}

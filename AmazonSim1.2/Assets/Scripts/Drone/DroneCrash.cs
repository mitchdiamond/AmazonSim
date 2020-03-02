using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCrash : MonoBehaviour {

    public int crashProbability = 100;
    private DroneData droneData;
    private DroneAlertSystem droneAlertSystem;
    public ParticleSystem smokePS;
    public ParticleSystem explodePS;

    public GameObject droneMesh;
    public float crashWaitTime = 2;

    private bool isCrashing = false;

    private void Start()
    {
        droneData = transform.parent.GetComponent<DroneData>();
        droneAlertSystem = transform.parent.GetComponent<DroneAlertSystem>();
    }

    public void StartCalcCrash()
    {
        StartCoroutine(CalcCrash());
    }

    IEnumerator CalcCrash()
    {
        while(droneData.isDispatched)
        {
            int randNum = Random.Range(1, crashProbability);

            if (randNum == 1)
            {
                droneAlertSystem.OnMechFailure();
                StartCrash();
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void StartCrash()
    {
        isCrashing = true;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        smokePS.Play();
    }


    private void OnCollisionEnter(Collision other)
	{
        if(isCrashing)
		{
            transform.parent.GetComponent<DroneHorizMovement>().StopDrone();
            GetComponent<DroneVertMovement>().enabled = false;
            explodePS.Play();
            StartCoroutine(OnCrash());
        }
	}

    IEnumerator OnCrash()
    {
        droneAlertSystem.OnDroneCrash();

        yield return new WaitForSeconds(crashWaitTime);

        droneMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;

        smokePS.Stop();
        explodePS.Stop();
        
        GameData.instance.IncreaseDestroyedDrones(1);

        DroneManager.instance.drones.Remove(transform.parent.gameObject);
        Destroy(transform.parent.GetComponent<DroneData>().droneButton);
        Destroy(transform.parent.GetComponent<DroneData>().droneFuelUI);
        DroneStatusButtonManager.instance.DeleteButton(droneData.droneStatusButton);
        Destroy(transform.parent.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneCreator : MonoBehaviour {

	public DroneStatusButtonManager droneStatusButtonManager;
	public DroneButtonManager droneButtonManager;
	public Camera mainCamera;
	public List<GameObject> dronePrefabs;
	public GameObject droneFuelPrefab;
	public GameObject hudPanel;
	public GameObject droneButtonPrefab;
	public GameObject droneSelectionWindow;

	public void CreateDrone()
	{
		//create new SO_Drone and set its values
		SO_Drone newDrone = ScriptableObject.CreateInstance<SO_Drone>();
		newDrone.droneNumber = DroneManager.instance.droneCounter + 1;
		DroneManager.instance.droneCounter++;

		//newDrone.droneType = droneType;
		int prefabNum = 0;
		
		// switch(droneType)
		// {
		// 	case SO_Drone.DroneType.BOXDRONE:
		// 		prefabNum = 0;
		// 		break;
		// 	case SO_Drone.DroneType.PLANEDRONE:
		// 		prefabNum = 1;
		// 		break;
		// 	case SO_Drone.DroneType.CURVEDRONE:
		// 		prefabNum = 2;
		// 		break;
		// }

		//create drone and add to drone manager
		GameObject newDroneObj = Instantiate(dronePrefabs[prefabNum], DroneManager.instance.warehousePads[newDrone.droneNumber - 1].GetChild(1).transform.position, Quaternion.identity);
		newDroneObj.GetComponent<DroneData>().sO_Drone = newDrone;
		DroneManager.instance.drones.Add(newDroneObj);

		//create drone fuel ui
		GameObject newDroneFuelObj = Instantiate(droneFuelPrefab);
		newDroneFuelObj.transform.SetParent(hudPanel.transform);
		newDroneFuelObj.GetComponent<UIFollowObj>().target = newDroneObj.transform.GetChild(1);
		newDroneFuelObj.GetComponent<UIFollowObj>().StartFollow();
		newDroneFuelObj.transform.SetAsFirstSibling();
		newDroneObj.GetComponent<DisplayUIImageFill>().image = newDroneFuelObj.transform.GetChild(0).GetComponent<Image>();
		newDroneObj.GetComponent<DroneData>().droneFuelUI = newDroneFuelObj;

		//get dronecamfollow working
		newDroneObj.transform.GetChild(1).GetComponent<DroneCamFollow>().mainCamera = mainCamera;

		//create and add drone button to ui
		GameObject newDroneButton = Instantiate(droneButtonPrefab);
		DroneButton thisDroneButton = newDroneButton.GetComponent<DroneButton>();

		newDroneButton.transform.SetParent(droneSelectionWindow.transform);
		newDroneButton.transform.localScale = Vector3.one;
		newDroneObj.GetComponent<DroneData>().droneButton = newDroneButton;

		//set values of new drone button
		thisDroneButton.sO_Drone = newDrone;
		thisDroneButton.buttonNumber = newDrone.droneNumber;
		thisDroneButton.Display();
		newDroneButton.GetComponent<Button>().onClick.AddListener(() => {droneButtonManager.OnButtonClicked(thisDroneButton.buttonNumber);});

		droneButtonManager.droneButtons.Add(thisDroneButton);

		//activate drone status button
		RenderTexture rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
		newDroneObj.GetComponent<DroneData>().dronePOVCam.targetTexture = rt;
		newDroneObj.GetComponent<DroneData>().droneStatusButton = DroneStatusButtonManager.instance.CreateButton(rt);
		newDroneObj.GetComponent<DroneData>().droneStatusButton.GetComponent<DroneCamFollow>().mainCamera = mainCamera;
		newDroneObj.GetComponent<DroneData>().droneStatusButton.GetComponent<DroneCamFollow>().objToFollow = newDroneObj.transform.GetChild(1);

		//set up drone status popup



	}
}
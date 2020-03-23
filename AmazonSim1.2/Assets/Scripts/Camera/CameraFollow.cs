using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform objToFollow;
	public bool canFollow = false;

	public float scrollSpeed = 50;
	public Vector2 zoomLimit;

	public float xBuffer;
	public float yBuffer;
	public float zBuffer;

	public float snapToY = 25;
	public float snapToZ = 20;

	public void StartFollow()
	{
		canFollow = true;
		StartCoroutine(Follow());
	}

	IEnumerator Follow()
	{
		yBuffer = snapToY;
		while(canFollow)
		{	
			Vector3 pos = transform.position;

			float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
			yBuffer -= scrollWheel * scrollSpeed * 100 * Time.deltaTime;
			yBuffer = Mathf.Clamp(yBuffer, zoomLimit.x, zoomLimit.y);

			zBuffer = 1.2f * yBuffer;

			pos.x = objToFollow.position.x - xBuffer;
			pos.y = objToFollow.position.y + yBuffer;
			pos.z = objToFollow.position.z - zBuffer;

			

			transform.position = pos;

			yield return null;
		}

	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowObj : MonoBehaviour {

	public Transform target;
	public bool canFollow;

	void Start()
	{
		canFollow = true;
		StartCoroutine(Follow());
	}

	IEnumerator Follow()
	{
		while(canFollow)
		{
			Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
			//print(screenPos);
     		transform.position = screenPos;
			yield return null;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour {

	private Animator anim;
	public int waitTimeMin;
	public int waitTimeMax;

	public List<SO_WeatherCondition> weatherConditions;

	public SO_WeatherCondition currentWeather;

	public List<string> animParameters;

	public void StartGame()
	{
		anim = GetComponent<Animator>();

		AnimatorControllerParameter[] parameters = anim.parameters;
		foreach (AnimatorControllerParameter param in parameters)
		{
			animParameters.Add(param.name);
		}

		StartCoroutine(ChooseWeather());

	}

	IEnumerator ChooseWeather()
	{
		while(true)
		{
			int weatherNum = Random.Range(0, anim.parameterCount - 1);
			int waitTime = Random.Range(waitTimeMin, waitTimeMax);
			ActivateWeather(weatherNum);
			yield return new WaitForSeconds(waitTime);
		}
	}

	public void ActivateWeather(int weatherNum)
	{
		print(animParameters[weatherNum]);
		anim.SetTrigger(animParameters[weatherNum]);
	}

	public void ChangeWeather(int weatherIndex)
	{
		currentWeather = weatherConditions[weatherIndex];
		GetComponent<DisplayUIImage>().Display(currentWeather.icon);
	}

}

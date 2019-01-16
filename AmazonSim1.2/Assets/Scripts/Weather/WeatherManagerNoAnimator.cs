using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class WeatherManagerNoAnimator : MonoBehaviour {

    //make sure list is sorted from lowest probability to highest probability
	public List<SO_WeatherCondition> weatherConditions;
    public SO_WeatherCondition currentWeather;

    public Animator anim;
    public List<string> animParameters;

    public Transform particleLocation;
    public List<ParticleSystem> particleSystems;

    public bool canChange = true;

	public void StartGame()
	{

		anim = GetComponent<Animator>();

		AnimatorControllerParameter[] parameters = anim.parameters;
		foreach (AnimatorControllerParameter param in parameters)
		{
			animParameters.Add(param.name);
		}

		//StartCoroutine(StartGameWait());
        StartCoroutine(ChooseWeather());
	}

	IEnumerator StartGameWait()
	{
		yield return new WaitForSeconds(60);
		StartCoroutine(ChooseWeather());
	}

	IEnumerator ChooseWeather()
	{
		while(canChange)
		{
			int randomNum = Random.Range(0, 100);
            int cumulative = 0;

            for (int i = 0; i < (weatherConditions.Count); i++)
            {
                print(i);
                cumulative += weatherConditions[i].weatherChance;

                if(randomNum < cumulative)
                {
                    currentWeather = weatherConditions[i];
                    print(currentWeather);
                    anim.SetTrigger(animParameters[i]);
                    ChangeWeather(i);
                    break;
                }
            }

			int waitTime = Random.Range(currentWeather.weatherTimeMin, currentWeather.weatherTimeMax);
			yield return new WaitForSeconds(5);
		}
	}

	public void ChangeWeather(int i)
	{
		GetComponent<DisplayUIImage>().Display(currentWeather.icon);
	}

}

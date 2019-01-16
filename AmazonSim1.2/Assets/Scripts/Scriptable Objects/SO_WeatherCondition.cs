using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]

public class SO_WeatherCondition : ScriptableObject {

	public string weatherName;
	public Sprite icon;
	public ParticleSystem weatherParticle;
	public ParticleSystem cloudsParticle;

	public int weatherTimeMin;
	public int weatherTimeMax;

	public int weatherChance;

	public bool isOvercast;


}

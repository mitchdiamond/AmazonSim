﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Destination", menuName = "Scriptable Objects/Destination") ]

public class SO_Destination : ScriptableObject {

	public string destName;
	public SO_Package destPkg;
	public float distance;

}
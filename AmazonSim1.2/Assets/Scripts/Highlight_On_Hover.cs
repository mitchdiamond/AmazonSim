using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight_On_Hover : MonoBehaviour {

	public Shader standard;
	public Shader highlight;
	bool mouseOver = false;



	void OnMouseEnter(){
	    mouseOver = true;
            GetComponent<Renderer>().material.shader = highlight;
	}

        void OnMouseExit(){
            mouseOver = false;
            GetComponent<Renderer>().material.shader = standard;
	}
}

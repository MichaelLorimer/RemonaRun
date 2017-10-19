using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackFlameSort : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Play";
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = -1; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

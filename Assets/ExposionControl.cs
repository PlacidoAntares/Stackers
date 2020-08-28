using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExposionControl : MonoBehaviour {

	public float duration;
	public bool isAwake;
	// Use this for initialization
	void Start () {
		isAwake = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isAwake == true) {
			InvokeRepeating ("Disable", duration, 0);
			isAwake = false;
		}
	}
	void Disable()
	{
		isAwake = false;
		this.gameObject.SetActive (false);
	}
}

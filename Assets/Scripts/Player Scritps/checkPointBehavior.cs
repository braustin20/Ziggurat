﻿using UnityEngine;
using System.Collections;

public class checkPointBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other){
		//Saves player location for future spawn after unfortunate and untimely death.
		PlayerPrefs.SetFloat ("PlayerX", this.transform.position.x);
		PlayerPrefs.SetFloat ("PlayerY", this.transform.position.y);
		PlayerPrefs.SetFloat ("PlayerZ", this.transform.position.z);
		Debug.Log ("Saved Checkpoint");
	}
	public void manualSave(){
		PlayerPrefs.SetFloat ("PlayerX", this.transform.position.x);
		PlayerPrefs.SetFloat ("PlayerY", this.transform.position.y);
		PlayerPrefs.SetFloat ("PlayerZ", this.transform.position.z);
		Debug.Log ("Manually Saved Checkpoint");
	}
}

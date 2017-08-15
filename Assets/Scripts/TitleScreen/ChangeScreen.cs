using UnityEngine;
using System.Collections;

public class ChangeScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		//check if action button is pressed, if so, choose whatever option the cursor is currently on
		if(Input.GetButtonDown("Submit")){
			Application.LoadLevel (Application.loadedLevel + 1);
		}

		if(Input.GetButtonDown("Back")) {
			Application.LoadLevel (Application.loadedLevel - 1);
		}
	}
}

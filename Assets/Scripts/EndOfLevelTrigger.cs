using UnityEngine;
using System.Collections;

//for triggering the end of level
public class EndOfLevelTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//when trigger is entered
	void OnTriggerEnter2D(Collider2D collider) {
		//if player reaches end of level, load the next level
		if (collider.tag == "Player") {
			Application.LoadLevel(3);
		}
	}
}

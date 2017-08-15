using UnityEngine;
using System.Collections;

public class EndBoss : MonoBehaviour {

	public GameObject boss;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (boss == null) {
			Application.LoadLevel(0);
		}
	}
}

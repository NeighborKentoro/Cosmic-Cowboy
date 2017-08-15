using UnityEngine;
using System.Collections;

public class SpaceshipCometLevel : MonoBehaviour {

	//the black hole's tranform for moving towards
	private Transform blackHole;

	//the x direction speed
	private float xSpeed;

	// Use this for initialization
	void Start () {
		blackHole = GameObject.Find ("BlackHole").transform;
		this.xSpeed = 0.015f;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		transform.position = Vector3.MoveTowards(transform.position, blackHole.position, this.xSpeed);

		xSpeed += 0.0001f;
	}
}

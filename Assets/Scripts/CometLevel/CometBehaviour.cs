using UnityEngine;
using System.Collections;

public class CometBehaviour : MonoBehaviour {

	//the black hole's tranform for moving towards
	private Transform blackHole;

	//the x direction speed
	private float xSpeed;

	//the speed of the rotation
	private float rotateSpeed;

	//the scale of the comet, use for both x and y
	private float scaleXY;

	// Use this for initialization
	void Start () {
		blackHole = GameObject.Find ("BlackHole").transform;
		xSpeed = 0.015f;//0.0001f (float)Random.Range (-400, -200);
		rotateSpeed = (float)Random.Range (0.5f, 2.2f);
		scaleXY = (float)Random.Range (1.0f, 3.0f);
		transform.localScale = new Vector3 (scaleXY, scaleXY, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate (){
		rigidbody2D.MoveRotation((float)(rigidbody2D.rotation + rotateSpeed));
		transform.position = Vector3.MoveTowards(transform.position, blackHole.position, xSpeed);
		xSpeed += 0.0001f;
		//rigidbody2D.AddForce(new Vector2(xSpeed, 0));
	}




}

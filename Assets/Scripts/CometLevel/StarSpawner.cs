using UnityEngine;
using System.Collections;

public class StarSpawner : MonoBehaviour {

	//the time counter
	private float timeCounter = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	//75, -51
	}

	void FixedUpdate () {
		if (Time.time > this.timeCounter) 
		{
			int ySpawn = Random.Range (75, -50);
			Vector3 spawnPoint = new Vector3(transform.position.x, ySpawn, transform.position.z);
			this.timeCounter = Time.time + 0.2f;
			Object instance = Instantiate(Resources.Load("StarCometLevel", typeof(GameObject)), spawnPoint, transform.rotation);
		}
	}
}

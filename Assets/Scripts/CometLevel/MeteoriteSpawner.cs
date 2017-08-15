using UnityEngine;
using System.Collections;

public class MeteoriteSpawner : MonoBehaviour {
	
	//the time till the next spawn
	private float tillNextSpawn;
	
	//the time counter
	private float timeCounter = 1;

	//is this the first spawn?
	private bool isFirstSpawn = true;
	
	// Use this for initialization
	void Start () {
		this.timeCounter += this.tillNextSpawn;
	}
	
	// Update is called once per frame
	void Update () {
		this.tillNextSpawn = Random.Range (1.7f, 2.7f);
		/*if(isFirstSpawn)
		{
			this.timeCounter += this.tillNextSpawn;
			this.isFirstSpawn = false;
		}*/
		if (Time.time > this.timeCounter) 
		{
			this.timeCounter = Time.time + this.tillNextSpawn;
			Object instance = Instantiate(Resources.Load("Meteorite", typeof(GameObject)), transform.position, transform.rotation);
		}
	}
}

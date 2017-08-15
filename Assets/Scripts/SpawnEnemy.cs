using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	//rigidbody of enemy instance
	public Rigidbody2D enemyInstance;

	//the number of spawns allowed at this point
	private int numOfSpawnsAllowed = 2;

	//the number of spawns done
	private int numOfSpawns = 0;

	//the time till the next spawn
	private float tillNextSpawn = 1;

	//the time counter
	private float timeCounter = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			if (this.numOfSpawns < this.numOfSpawnsAllowed && Time.time > this.timeCounter) {
				this.timeCounter = Time.time + this.tillNextSpawn;
				Rigidbody2D enemy = Instantiate(enemyInstance, transform.position, transform.rotation) as Rigidbody2D;
				this.numOfSpawns += 1;
			}
		}
	}
}

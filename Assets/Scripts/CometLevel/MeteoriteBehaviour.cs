using UnityEngine;
using System.Collections;

public class MeteoriteBehaviour : MonoBehaviour {

	//the x speed of the meteorites
	private float xSpeed;

	//the y speed of the meteorites
	private float ySpeed;

	// Use this for initialization
	void Start () {
		this.xSpeed = Random.Range (-1.3f, -1.7f);
		this.ySpeed = Random.Range (-1.3f, -1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector2 (this.xSpeed, this.ySpeed)); //-1.1, -0.6
		//rigidbody2D.AddForce (new Vector2 (-30, -25));
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerControl>().takeDamage(10);
			Destroy(gameObject);
		}
		else if(other.gameObject.tag == "LowerBounds" || other.gameObject.tag == "BlackHole")
		{
			Destroy (gameObject);
		}
	}
}

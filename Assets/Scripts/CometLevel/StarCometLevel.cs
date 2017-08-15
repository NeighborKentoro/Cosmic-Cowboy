using UnityEngine;
using System.Collections;

public class StarCometLevel : MonoBehaviour {

	//the animation number
	private int animNumber;

	//the speed the star flies by
	private float xSpeed;
	
	// Use this for initialization
	void Start () {
		//determine the animation used
		this.animNumber = (int) Random.Range (0, 2);
		Animator starAnim = gameObject.GetComponent<Animator> ();
		starAnim.SetInteger("starAnimation", animNumber);

		//determine the speed of the star
		this.xSpeed = Random.Range(-1, -4);
	}
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		//move the star based on its speed
		transform.Translate (new Vector3 (this.xSpeed, 0, 0));
	}

	void OnTriggerEnter2D (Collider2D other) {
		if(other.tag == "BlackHole"){
			Destroy (gameObject);
		}
	}
}

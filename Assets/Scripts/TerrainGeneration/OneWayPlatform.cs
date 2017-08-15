using UnityEngine;
using System.Collections;

public class OneWayPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag=="Player"||coll.tag=="Enemy")
			Physics2D.IgnoreCollision (this.collider2D, coll, true);
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.tag=="Player"||coll.tag=="Enemy")
			Physics2D.IgnoreCollision (this.collider2D, coll, false);
	}

}

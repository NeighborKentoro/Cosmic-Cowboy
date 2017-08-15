using UnityEngine;
using System.Collections;

public class CannonLeft : CannonBehavior {

	// Use this for initialization
	void Start () {
		base.Start ();
		pattern = new bool[] {true,true,true,false};
		bulletVelocity = new Vector2 (-10, 0);
		fireRate = 45;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}

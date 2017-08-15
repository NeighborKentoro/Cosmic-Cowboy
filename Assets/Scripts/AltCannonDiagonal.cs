using UnityEngine;
using System.Collections;

public class AltCannonDiagonal : CannonBehavior {
	
	// Use this for initialization
	void Start () {
		base.Start ();
		pattern = new bool[] {true,true,true,false};
		bulletVelocity = new Vector2 (-10f, 10f);
		fireRate = 45;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
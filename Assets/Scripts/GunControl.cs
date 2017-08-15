using UnityEngine;
using System.Collections;

//From the tutorial with changes by Kent Weiler

public class GunControl : MonoBehaviour {

	//the Rigidbody2D of the bullet to fire from the gun
	public Rigidbody2D bullet; 
		
	//the script of the player
	private PlayerControl script;

	//the time until the next fire is allowed
	private float timeTillNextFire = 0.4f;

	//the time counter for checking firing rate
	private float timeCounter;
		
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find("/CosmicCowboy") ;
		this.script = player.GetComponent<PlayerControl> ();
	}
		
	// Update is called once per frame
	void Update () {

		//if the player pressed the button to fire a bullet
		if(Input.GetButtonDown("Fire1") && Time.time >= timeCounter) {
			this.timeCounter = Time.time + timeTillNextFire;

			this.script.SetIsIdleFiring(true);

			//create a new vector for reposition bullet when instantiated
			Vector3 repositionBullet = transform.position;
			repositionBullet.y -= 1.3f;

			//is the player facing right?
			if(script.GetIsFacingRight())
			{
				//move to right of gun
				repositionBullet.x += 1.8f;

				if(script.IsFlying()) {
					//create and reposition a bullet for a better look when flying
					Rigidbody2D bulletInstance = Instantiate(bullet, repositionBullet, transform.rotation) as Rigidbody2D;
				} else {
					//create a new bullet that fires right
					Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody2D;
				}
			}
			//the player is facing left
			else
			{
				//move to left of gun
				repositionBullet.x -= 2.0f;

				if(script.IsFlying()) {
					//create and reposition a bullet for a better look when flying
					Rigidbody2D bulletInstance2 = Instantiate(bullet, repositionBullet, transform.rotation) as Rigidbody2D;
				} else {
					//create a new bullet that fires left
					Rigidbody2D bulletInstance2 = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody2D;
				}
			}
		}
	}
}

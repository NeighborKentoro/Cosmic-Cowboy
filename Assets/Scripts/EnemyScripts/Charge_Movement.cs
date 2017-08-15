using UnityEngine;
using System.Collections;

public class Charge_Movement : EnemyClass {


	//float for movement speed of sprite
	private float speed = 10f;

	protected Animator chargeAnim;

	protected int counter;

	protected bool charging;

	// initialization of setting variables, also flips the sprite
	void Start () {
		facingRight = true;
		FlipNeeded(true);
		sawPlayer = false;
		setHealth(15);
		setDamage (10);
		setDistThres (20);
		setKnockback (new Vector2 (0, 50));
		chargeAnim = GetComponent<Animator>();
		counter = 0;
		canFlipOnHit = true;
		Player = GameObject.FindGameObjectWithTag ("Player");
		charging = false;
	}
	
	// Update is called once per frame
	void Update () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		if (isDead ()) {
			base.Death();
			determineDistFlag();
		}
		//if the creature has seen the player it charges at it
		if(isWithinDist())
		{
			if(!charging)
			{
				chargeAnim.SetInteger("Charge_State", 1);
				charging = true;
			}
			FlipCheck();
			print ("charging");
			if (facingRight)
			{
				rigidbody2D.velocity = new Vector2 ( speed, rigidbody2D.velocity.y);
			}
			else
			{
				rigidbody2D.velocity = new Vector2 ( -speed, rigidbody2D.velocity.y);
			}
		}
		else
		{
			chargeAnim.SetInteger("Charge_State", 0);
			charging = false;
		}
	}

	//after enemy has landed check if the player is behind him and turns around
	//then checks if the player is still in range, if so then jump again, else do idle
	void FlipCheck()
	{
		
		if(facingRight && Player.transform.position.x < transform.position.x)
		{
			Flip();
		}
		if(!facingRight && Player.transform.position.x > transform.position.x)
		{
			Flip ();
		}
		counter = 0;
	}

	


}

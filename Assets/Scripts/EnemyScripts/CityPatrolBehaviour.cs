using UnityEngine;
using System.Collections;

public class CityPatrolBehaviour : EnemyClass {
	//Delegate variables
	delegate void myDelegate();
	myDelegate enemyAction;

	//Animator
	protected Animator anim;

	//Variables for patrol 
	public float patrolSpeed;
	public float directionSpeed = 200f;
	float xPos; //Enemy's x position
	public float distance = 4f; //Range within enemy patrols

	// Use this for initialization
	void Start () {
		//Enemy class vars
		anim = GetComponent<Animator> ();
		facingRight = false;
		sawPlayer = false;
		setDamage(10);
		setHealth(10);
		setKnockback (new Vector2 (100, 50));
		dead = false;
		setDistThres(10);
		setDistFlag(false);

		//Vars for patrol sequenc
		xPos = transform.position.x; //Get the enemy's x position
		patrolSpeed = -directionSpeed; //Set patrolSpeed

		enemyAction = idlePatrol; //Set enemyAction to patrol sequences; "root node"
	}
	
	// Update is called once per frame
	void Update () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		determineDistFlag();
		enemyAction(); //Carry out whatever action is in the delegate
	}

	// Enemy will patrol a set distance until Player comes into view
	public void idlePatrol() {
		if (xPos - transform.position.x > distance) {
			patrolSpeed = directionSpeed; //Change direction
			if(!facingRight) //Flip the enemy if necessary
				Flip ();
			facingRight = true;
		}
		
		else if (xPos - transform.position.x < -distance) {
			patrolSpeed = -directionSpeed; //Change direction
			if(facingRight) //Flip the enemy if necessary
				Flip ();
			facingRight = false;
		}

		transform.Translate((patrolSpeed*Time.deltaTime*2), 0, 0); //Move between the two points
	}
	
	public void attackPlayer() {
		Debug.Log ("Attack");

		PlayerControl playerScript = GameObject.FindGameObjectWithTag ("Player").transform.GetComponent<PlayerControl>();
		if(playerScript.GetIsFacingRight() && facingRight) //Flip based on the player's direction
			Flip();
		else if(!playerScript.GetIsFacingRight() && !facingRight) //Flip based on the player's direction
			Flip ();
	}

	public override void determineDistFlag()
	{
		if (isWithinDist() && !getDistFlag()) 
		{
			setDistFlag(true);
			anim.SetInteger("Detected", 1);
			//Player is detected, so attack
			enemyAction = attackPlayer;
		}
		if(!isWithinDist() && getDistFlag())
		{
			setDistFlag(false);
			anim.SetInteger("Detected", 0);
			//Player is out of range, so go back to patrolling
			enemyAction = idlePatrol;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//Take damage if you get hit
		if (coll.gameObject.tag == "Bullet") {
			takeDamage(2);
		}
				
		//Death
		if (isDead() && coll.gameObject.tag == "Bullet") {
			Death (); 
		}
	}
}

using UnityEngine;
using System.Collections;

public class Pounce_Movement : EnemyClass{
	//move speed of spirte
	private float speed = 15f;
	//bool for if the sprite is in an alert state
	private bool alert;
	//bool for if the sprite is in a pounce state
	private bool pounce;
	//Animator object
	private Animator anim;
	//variable counter for states
	private int counter;
	//bool flag for grounded, mainly for maintaining pouncing animation
	private bool grounded;
	//Vector for pouncing left
	private Vector2 leftPounceForce;
	//Vector for pouncing right
	private Vector2 rightPounceForce;

	delegate void myDelegate();

	myDelegate enemyAction;

	protected Animator pounce_anim;



	//intialization of variables
	void Start () {
		facingRight = false;
		sawPlayer = false;
		alert = false;
		pounce = false;
		grounded = true;
		counter = 0;
		rightPounceForce = new Vector2(800, 1600);
		leftPounceForce = new Vector2 (-800, 1600);
		setDamage(5);
		setHealth(10);
		setKnockback (new Vector2 (20, 20));
		dead = false;
		setCanFlipOnHit (true);
		enemyAction = Idle;
		pounce_anim = GetComponent<Animator>();
		pounce_anim.SetInteger ("Pounce_State", 0);
		setDistThres (25);
		Player = GameObject.FindGameObjectWithTag ("Player");
		enemyAction = Idle;
		Jump ();

	}
	
	// Update is called once per frame
	void Update () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		if (isDead ()) {
			base.Death();
		}
		determineDistFlag();
		enemyAction();

	}
	//dealing with alert and pounce anim ::: NOT DONE YET 
	void FixedUpdate()
	{

	}
	

	void Idle()
	{
		pounce_anim.SetInteger("Pounce_State", 0);
		if(isWithinDist())
		{
			enemyAction = JumpMotion;
			counter = 0;
		}

	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		base.OnCollisionEnter2D (coll);
		if(pounce && (coll.transform.tag == "Ground"||coll.transform.tag == "Platform"))
		{
			enemyAction = Land;
			counter = -1;
			pounce_anim.SetInteger("Pounce_State", 4);
		}
	}

	void Jump()
	{
		pounce_anim.SetInteger("Pounce_State", 1);
		if(facingRight)
		{
			rigidbody2D.AddForce(rightPounceForce);
		}
		else
		{
			rigidbody2D.AddForce(leftPounceForce);
		}
		pounce = true;
		grounded = false;
	}

	void JumpMotion()
	{
		if(!pounce && counter == 0)
		{
			Jump ();
		}
		if(pounce && counter == 5)
		{
			pounce_anim.SetInteger("Pounce_State", 2);
		}
		if(pounce && counter == 10)
		{
			pounce_anim.SetInteger("Pounce_State", 3);
		}
		counter++;
	}

	void Land()
	{
		if(counter == 0)
		{
			pounce_anim.SetInteger("Pounce_State", 4);
			rigidbody2D.velocity = new Vector2(0, 0);
		}
		if(counter == 20)
		{
			enemyAction = LandCheck;
			counter = -1;
			pounce = false;
			grounded = true;

		}
		counter++;
	}

	void LandCheck()
	{

		if(facingRight && Player.transform.position.x < transform.position.x)
		{
			Flip();
		}
		if(!facingRight && Player.transform.position.x > transform.position.x)
		{
			Flip ();
		}
		if(isWithinDist())
		{
			enemyAction = JumpMotion;
		}
		else
		{
			enemyAction = Idle;
		}
		counter = 0;
	}


}

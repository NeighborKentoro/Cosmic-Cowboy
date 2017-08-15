using UnityEngine;
using System.Collections;

//

public class PlayerControl : MonoBehaviour, IsDamagable<int>, IsKillable{

	//the animator for changing animations
	private Animator anim; 

	//for horizontal axis control
	private float horizontal;

	//for vertical axis control
	private float vertical;

	//determines idle firing
	private bool isIdleFiring = false;

	//for determining the direction to shoot
	private bool isFacingRight = true;

	//for setting the speed of running on the ground
	public float groundSpeed = 9.0f;

	public int gravity = 6;

	//for setting the speed of flying in the air
	public float airSpeed = 15.0f;

	//for determining the layer to ground check on
	public LayerMask whatIsGrounded;

	//the amount of force in the jump
	public float jumpForce = 1500;

	//whether or not the player is grounded
	private bool isGrounded = false;

	//whether or not the player is flying
	private bool isFlying = false;

	//the transform to check for the ground underneath the player
	public Transform groundCheck;

	//for adjusting the player's speed
	private float speed;

	//the players vertical flying speed
	public float liftDropSpeed = 10.0f;

	//the players health
	protected int playerHealth = 100;

	//the damage the player can deal
	protected int damage = 5;

	//if player has been knocked back it disables movement for a few seconds
	protected bool isKnockedBack = false;

	//Velocity vector of current knockBack;
	protected Vector2 knockback;

	protected int knockBackCounter = 0;

	public int knockBackTime = 30;

	protected int heartbeat = 0;

	//refence to character's box collider to change for walking to flying state and vice versa
	protected BoxCollider2D hitBox;

	//the amount of time the jet
	protected float jetTime = 110;

	//the amount of time before the jetTime refills
	protected float jetRefillTime = 1;

	//the jetTime refill counter
	protected float jetTimeCounter = 0;

	FlashInvisible flash;

	// 0 is jump
	// 1 is cry
	// 2 is death
	public AudioClip jumpSound;
	public AudioClip hurtSound;
	public AudioClip deathSound;

	// Use this for initialization
	void Start () {

		//initialize the animator
		this.anim = rigidbody2D.GetComponent<Animator>();

		//get the boxcollider2d of the player
		this.hitBox = GetComponent<BoxCollider2D>();

		flash = GetComponent<FlashInvisible> ();

	}
	
	// Update is called once per frame
	void Update () {

		//Check whether player is grounded
		bool check1 = Physics2D.Linecast (new Vector2 (groundCheck.position.x - 0.8f,groundCheck.position.y - 0.3f), new Vector2 (groundCheck.position.x, groundCheck.position.y - 0.6f), 1 << LayerMask.NameToLayer ("Ground"));
		bool check2 = Physics2D.Linecast (new Vector2 (groundCheck.position.x +0.8f,groundCheck.position.y - 0.3f), new Vector2 (groundCheck.position.x, groundCheck.position.y - 0.6f), 1 << LayerMask.NameToLayer ("Ground"));
		this.isGrounded = check1||check2;
		//Check for jumping and flying
		this.JumpAndFly ();
		//checks after being hit, movement is disabled for two seconds and when two seconds has passed the player can move again
		if(isKnockedBack)
		{
			if(heartbeat == 120)
			{
				heartbeat = 0;
				setIsKnockedBack(false);
				this.anim.enabled = true;
			}
			heartbeat++;
		}
	}


	void FixedUpdate () {

		//Get values of horizontal and vertical axis
		this.horizontal = Input.GetAxis ("Horizontal");
		this.vertical = Input.GetAxis ("Vertical");

		//Speed of player based on whether or not player is flying
		if (!getIsKnockedBack ())
		{
			this.determinePlayerSpeed ();
		}
		else 
		{
			if(knockBackCounter==0)
				rigidbody2D.velocity = knockback;
			knockBackCounter++;
			if(knockBackCounter==knockBackTime)
			{
				isKnockedBack = false;
				knockBackCounter = 0;
			}
		}

		//change the animation based on movement
		this.determinePlayerAnimation ();

		//determine the direction the player is facing
		if ( !getIsKnockedBack() && (horizontal > 0 && !isFacingRight) || (horizontal < 0 && isFacingRight) ) {
			this.Flip();
		}

		this.CheckJetTimeRefill ();

		//reseting the ground check
		this.isGrounded = false;

	}

	//for flipping the direction the player is facing for shooting/walking/flying
	void Flip () {
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	//The function for checking for Jumping and Flying
	private void JumpAndFly () {
		//Make player jump
		if (isGrounded && Input.GetButtonDown("Jump")){
			//play jump sound
			audio.clip = jumpSound;
			audio.Play();
			rigidbody2D.AddForce (new Vector2 (0, jumpForce));

			//switch to jumping animation
			//anim.SetInteger("currentAnimation", 0);
		}
		
		//Make player stop flying
		if (isFlying && Input.GetButtonDown("Jump") || this.jetTime <= 0) {
			this.isFlying = false;
			//change the hitbox size back to his upright position
			this.hitBox.size = (new Vector2(1.2f, 6.9f));

			//turn on the circle collider so it doesn't hit the ground
			gameObject.GetComponent<CircleCollider2D>().isTrigger = false;

			//switch to jumping animation
			//anim.SetInteger("currentAnimation", 4); 
			
			//Make player start flying
		} else if (!isGrounded && !isFlying && Input.GetButtonDown("Jump") && this.jetTime > 0) {
			this.isFlying = true;

			//switch to flying animation, which is 2
			this.anim.SetInteger("currentAnimation", 2);

			//change the hitbox size to fit the flying position
			this.hitBox.size = (new Vector2(6.2f, 2.9f));

			//turn off the circle collider for better ground colliding
			gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
		//player got too close to the ground so shutoff the jetpack
		} else if (isGrounded && isFlying) {
			isFlying = false;

			//change the hitbox size back to his upright position
			this.hitBox.size = (new Vector2(1.2f, 6.9f));

			//turn on the circle collider for better ground colliding
			gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
		}
	}


	//Setters and Getters
	//Returns the isFacingRight boolean
	public bool GetIsFacingRight () {
		return this.isFacingRight;
	}
	
	//Sets the isIdleFiring boolean
	//Parameter idleFiring - the new value of isIdleFiring
	public void SetIsIdleFiring (bool idleFiring) {
		this.isIdleFiring = idleFiring;
	}

	//set the health of the player
	public void setHealth(int health)
	{
		this.playerHealth = health;
	}

	//get the health of the player
 	public int getHealth()
	{
		return this.playerHealth;
	}

	//
	public void takeDamage(int damageTaken)
	{
		if(!getIsKnockedBack())
		{


			flash.BeenHit=true;
			flash.dead = isDead();
			this.playerHealth -= damageTaken;

			//check for player death
			if (isDead() == true) {
				// play death
				audio.clip = deathSound;
				audio.Play();
				//player is dead so kill him
				Death();
			}
			else
			{
				// play cry
				audio.clip = hurtSound;
				audio.Play();
			}
		}
	}

	//check to see if the player is dead
	public bool isDead()
	{
		//if the player's health is 0 he is dead
		if(this.playerHealth <= 0)
		{
			return true;
		}
		//else he is still alive
		else
		{
			return false;
		}
	}
	
	public void Death()
	{
		//do death animation
		Application.LoadLevel (Application.loadedLevel);
	}

	//sets isKnockedBack boolean
	public void setIsKnockedBack(bool knockBack)
	{
		isKnockedBack = knockBack;
	}
	
	//gets isKnockedBack boolean
	protected bool getIsKnockedBack ()
	{
		return isKnockedBack;
	}

	//checks the state of the player and determines the player's move speed
	private void determinePlayerSpeed() {
		//player is flying so move faster
		if (isFlying) {
			rigidbody2D.gravityScale = 0;
			this.speed = airSpeed;

			if(!isKnockedBack)
			{
				rigidbody2D.velocity = new Vector2 (horizontal * speed, vertical * liftDropSpeed);
			}
		//player is on ground so move slower
		} else {
			rigidbody2D.gravityScale = gravity;
			this.speed = groundSpeed;

			if(!isKnockedBack)
			{
				rigidbody2D.velocity = new Vector2 (horizontal * speed, rigidbody2D.velocity.y);
			}
		}
	}

	//checks the state of the player and determines the current animation needed
	private void determinePlayerAnimation() {

		if(getIsKnockedBack())
		{
			//if player has been hit disabled animation to disable movement
			//this.anim.enabled = false;
		}
		else if(!isFlying && !isGrounded && transform.rigidbody2D.velocity.y > 0)
		{
			//if player isn't flying and isn't grounded and their y velocity is positive play jump up animation.
			this.anim.SetInteger("currentAnimation", 4);
		}
		else if(!isFlying && !isGrounded && transform.rigidbody2D.velocity.y < 0)
		{
			//if player isn't flying and isn't grounded and their y velocity is negative play jump down animation
			this.anim.SetInteger("currentAnimation", 5);
		}
		else if ((horizontal > 0) || (horizontal < 0)) {
			
			//if player is not flying, then he is walking
			if(!isFlying) {
			//switch to walking animation, which is 1
			this.anim.SetInteger ("currentAnimation", 1);
		}
		//player is idle but firing
		} else if (!isFlying && isIdleFiring) {
			
			this.anim.SetInteger ("currentAnimation", 3);
			this.isIdleFiring = false;
			
			//else the character is idle, so play the idle animation
		} else {
			
			if(!isFlying) {
				//switch to idle animation, which is 0
				this.anim.SetInteger ("currentAnimation", 0);
			}
		}
	}

	//returns the isFlying boolean
	public bool IsFlying () {
		return isFlying;
	}

	//returns the amount of jet time
	public float GetJetTime () {
		return this.jetTime;
	}

	//check to see if jetTime can refill
	private void CheckJetTimeRefill () {

		//subtract jet time if flying
		if (isFlying) {
			this.jetTime -= 0.5f;
			this.jetTimeCounter = 0;
		}
		//add to jet time
		else if (this.jetTime < 110) {
			//if the refill counter has been passed, refill
			if (this.jetTimeCounter > this.jetRefillTime) {
				this.jetTime += 0.5f;
			}
			//else add to the time counter
			else 
			{
				this.jetTimeCounter += 0.015f;
			}
		}
		//reset the time counter
		else
		{
			this.jetTimeCounter = 0;
		}

	}

	//check for collisions with enemies
	void OnCollisionEnter2D (Collision2D collider) {
		if (collider.gameObject.tag == "Enemy") {
			EnemyClass enemyScript = collider.transform.GetComponent<EnemyClass>();

		}
	}

	public void KnockBack(Vector2 inknockBack)
	{
		isKnockedBack = true;
		knockback = inknockBack;
	}
	
}

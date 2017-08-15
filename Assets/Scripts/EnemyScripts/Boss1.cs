using UnityEngine;
using System.Collections;

public class Boss1 : EnemyClass, IsDamagable<int>, IsDamager<int>, IsKillable{

	//reference to the Animator connected to the head
	protected Animator headAnim;
	//reference to BodyAnimation script, which deals with animating the body
	public BodyAnimation bodyAnim;
	//variable to keep track of what anim state the head is in
	protected int headState;
	//variable for what anim state the body is in
	protected int currBodyState;
	//variable used for time checks
	protected float timeCheck;
	//public reference to a transform to represent the 
	//location of the left side of the level floor
	public Transform LeftRoom;
	//public reference to a transform to represent the 
	//location of the right side of the level floor
	public Transform RightRoom;
	//public reference to a transform to represent the 
	//location of the middle area of the level.
	public Transform MidRoom;
	//an array that holds all the room locations
	public Transform[] Points;
	//int to represent which Points location the boss is at
	//0 is left side, 1 is the middle of the room, 2 is the right side
	protected int currPosition = 2;
	//determines which area the boss has to travel to.
	protected int nextPosition = 1;
	//variable for bullet move speed
	float moveSpeed = 5f;
	//variable that determines when the next head animation occurs
	int healthThreshold = 130;
	//public reference to the GameObject for the bullets the boss shoots
	public GameObject Bullet;
	//public reference to the Transform of where the bullets and blasts spawn
	public GameObject BulletSpawn;
	//public reference to the GameObject of the blast the boss shoots
	public GameObject Blast;
	//game counter
	public float counter;
	//used for state control in fireBullets() function
	public int shotIter;
	//adds this modifier to the body animation arguments
	//if health is low then modifier equals 7
	public int isLowModifier = 0;
	//delegate declaration
	protected delegate void ThisDelegate();
		//delegate variable
	protected ThisDelegate BossDecision;

	protected bool shotOnce;

	protected bool hasBlasted;

	protected bool isWalking;
	// intialize all start values
	void Start () {
		setDamage(5);
		setHealth(150);
		headAnim = GetComponent<Animator>();
		bodyAnim = GetComponentInChildren<BodyAnimation>();
		bodyAnim.setHealth(getHealth ());
		bodyAnim.prevHealth = getHealth();
		headState = 0;
		timeCheck = Time.time;
		headAnim.SetInteger("faceSwitch", headState);
		Points = new Transform[3] {LeftRoom, MidRoom, RightRoom};
		facingRight = false;
		currBodyState = 7 + isLowModifier;
		bodyAnim.setRoboState(currBodyState);
		bodyAnim.setIsLow (true);
		counter = Time.time;
		BossDecision = beginState;
		shotIter = 0;
		facingRight = false;
		setCanFlipOnHit (false);
		shotOnce = false;
		hasBlasted = false;
		isWalking = false;
	}


	
	//function to deal with object's death
	public void Death()
	{
		this.collider2D.isTrigger = true;
		Destroy(this.gameObject, 5);
		//do animation
	}

	// check if player is dead, if health of body doesn't match health of head
	//then update head health to body health, if health below a threshold call function
	//set delegate to BossDecision function
	void Update () {
		if(isDead())
		{
			Death();
		}
		if(getHealth() != bodyAnim.getHealth())
		{
			setHealth(bodyAnim.getHealth());
		}
		if(getHealth() <= healthThreshold)
		{
			updateThreshold();
		}
		BossDecision();
	}

	//begin state does nothing for about 5 seconds
	//then sets delegate to fireBullets
	void beginState()
	{
		if((Time.time - counter) > 3f)
		{
			counter = Time.time;
			BossDecision = fireBullets;
		}
	}

	//is called when health reahces 1/3 of hp or 50 hp
	//robot body now does blinking anims, by changing isLowModifer
	//doubles speed
	void lowHealth()
	{
		isLowModifier = 7;
		bodyAnim.setRoboState (currBodyState + isLowModifier);
		moveSpeed = 10f;
	}

	void spawnBullet()
	{
		float bulletScatter = Random.Range(-15, 15);
		float scatterIncrX = Mathf.Abs(500 * (bulletScatter/180));
		float scatterIncrY = scatterIncrX;
		if(bulletScatter > 0)
		{
			scatterIncrY *= -1;
		}

		GameObject blast1 = Instantiate (Bullet, BulletSpawn.transform.position, transform.rotation) as GameObject;
		if(facingRight)
		{
			blast1.rigidbody2D.AddForce(new Vector2(500 - scatterIncrX, scatterIncrY));
		}
		else
		{
			blast1.rigidbody2D.AddForce(new Vector2(-500 - scatterIncrX, scatterIncrY));
		}
	}

	void spawnBlast()
	{
		GameObject blast2 = Instantiate (Blast, BulletSpawn.transform.position, transform.rotation) as GameObject;
		if(facingRight)
		{
			blast2.rigidbody2D.AddForce(new Vector2(500, 0));
			Vector3 theScale = blast2.transform.localScale;
			theScale.x *= -1;
			blast2.transform.localScale = theScale;
		}
		else
		{
			blast2.rigidbody2D.AddForce(new Vector2(-500, 0));
		}
	}


	
	//function to fire bullets
	void fireBullets()
	{

		if(shotIter == 0)
		{	currBodyState = 6 + isLowModifier;
			bodyAnim.setRoboState(currBodyState);
			timeCheck = Time.time;
			shotIter++;
		}
		else
		{
			if(!shotOnce && (Time.time - timeCheck) > 0.3f )
			{
				spawnBullet();
				spawnBullet();
				spawnBullet();
				shotOnce = true;
			}
			if(shotOnce && (Time.time - timeCheck) > 1f)
			{
				spawnBullet();
				spawnBullet();
				spawnBullet();
				currBodyState = 7 + isLowModifier;
				bodyAnim.setRoboState(currBodyState);
				shotIter = 0;
				shotOnce = false;
				timeCheck = Time.time;
				BossDecision = fireBlasts;
			}
		}
	}

	//called when health meets the current health threshold
	//goes to next head animation and sets the threshold to threshold-20
	//is called everytime enemy takes 20 damage, which changes the head anim
	//when health hits 50 headAnim is final state, and body anims now blink
	void updateThreshold()
	{
		if(enemyHealth <= 50)
		{
			headState = 5;
			headAnim.SetInteger("faceSwitch", headState);
			lowHealth();
		}
		else
		{
			healthThreshold -= 20;
			headState++;
			headAnim.SetInteger("faceSwitch", headState);
		}
	}


	//function to fire blasts
	void fireBlasts()
	{
		if(!hasBlasted && (Time.time - timeCheck) > 0.01f)
		{
			currBodyState = 3 + isLowModifier;
			bodyAnim.setRoboState(currBodyState);
		}
		if(!hasBlasted && (Time.time - timeCheck) > 0.42f)
		{
			currBodyState = 4 + isLowModifier;
			bodyAnim.setRoboState(currBodyState);
			spawnBlast();
			hasBlasted = true;
		}
		if(hasBlasted && (Time.time - timeCheck) > 2f)
		{
			currBodyState = 5 + isLowModifier;
			bodyAnim.setRoboState(currBodyState);
		}

		if(Time.time - timeCheck > 2.3f)
		{
			currBodyState = 7 + isLowModifier;
			bodyAnim.setRoboState(currBodyState);
			timeCheck = Time.time;
			hasBlasted = false;
			BossDecision = determineNextPosition;
		}

	}

	//boss determines which of the 5 points it should move to next based on player position
	void determineNextPosition()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		float minDist = 100f;
		float tempDist;
		for(int i = 0; i < 3; i++)
		{
			tempDist = Vector3.Distance(Player.transform.position, Points[i].transform.position);
			if(tempDist < minDist)
			{
				minDist = tempDist;
				nextPosition = i;
			}
		}
		timeCheck = Time.time;
		BossDecision = MoveToNextSpot;
	}
	//checks if the enemy has reached its destination,
	//if not then determine what your route of travel is to the next point
	void MoveToNextSpot()
	{
		if (nextPosition == currPosition)
		{
			Player = Player = GameObject.FindGameObjectWithTag("Player");
			if(facingRight && (Player.transform.position.x < transform.position.x))
			{
				Flip();
			}
			if(!facingRight && (Player.transform.position.x > transform.position.x))
			{
				Flip();
			}
			BossDecision = fireBullets;
		}
		else if (currPosition == 0)
		{
			BossDecision = WalkRight;
		}
		else if (currPosition == 1)
		{
			if(nextPosition == 0)
			{
				BossDecision = WalkLeft;
			}
			else
			{
				BossDecision = WalkRight;
			}
		}
		else 
		{
			BossDecision = WalkLeft;
		}

	}
	//script for walking to the left, can be done from point 1 to 2 or 2 to 3
	void WalkLeft()
	{
		if(!isWalking)
		{
			currBodyState = 1 + isLowModifier;
			bodyAnim.setRoboState(currBodyState);
			timeCheck = Time.time;
			isWalking = true;
		}

		if(Time.time-timeCheck > 0.3f)
		{

			if(transform.position.x - Points[nextPosition].position.x > 0.1f)
			{

				transform.Translate(new Vector3(-1 * moveSpeed * Time.deltaTime, 0, 0));
			}
			else
			{
				Vector3 tempPos1 = transform.position;
				tempPos1.x = Points[nextPosition].position.x;
				transform.position = tempPos1;
				currBodyState = 7 + isLowModifier;
				bodyAnim.setRoboState(currBodyState);
				currPosition = nextPosition;
				timeCheck = Time.time;
				isWalking = false;
				BossDecision = MoveToNextSpot;

			}
		}
	}
	//function for walking to the right, can be done from point 3 to 2, or 2 to 1
	void WalkRight()
	{
		if(!isWalking)
		{
			currBodyState = 1 + isLowModifier;
			bodyAnim.setRoboState(currBodyState);
			timeCheck = Time.time;
			isWalking = true;

		}
		
		if(Time.time-timeCheck> 0.3f)
		{
			if(Points[nextPosition].position.x  - transform.position.x > 0.1f)
			{
				transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
			}
			else
			{
				Vector3 tempPos1 = transform.position;
				tempPos1.x = Points[nextPosition].position.x;
				transform.position = tempPos1;
				currBodyState = 7 + isLowModifier;
				bodyAnim.setRoboState(currBodyState);
				currPosition = nextPosition;
				timeCheck = Time.time;
				isWalking = false;
				BossDecision = MoveToNextSpot;

			}
		}
	}




	void determineOrientation()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		if(facingRight)
		{
			if(Player.transform.position.x < transform.position.x)
			{
				Flip();
			}
		}
		else
		{
			if(Player.transform.position.x > transform.position.x)
			{
				Flip();
			}
		}
		BossDecision = determineNextPosition;
	}

	public void Flip()
	{
		if(facingRight)
		{
			facingRight = false;
		}
		else
		{
			facingRight = true;
		}
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}



}

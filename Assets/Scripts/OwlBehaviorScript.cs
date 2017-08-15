using UnityEngine;
using System.Collections;


public class OwlBehaviorScript : EnemyClass {
	delegate void myDelegate();
	myDelegate enemyAction;
	
	PlayerControl script;
	
	float speed = 1.5f;
	
	
	bool reset = false;
	Animator anim;
	
	//Threshhold Values
	int range = 30;
	int visible = 50;
	
	
	
	// Use this for initialization
	void Start () {
		enemyAction = targetVisible;
		anim = GetComponent<Animator> ();
		setHealth (9);
		setDamage (5);
		setKnockback (new Vector2 (10, 10));
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead ()) {
			base.Death();
		}
		Player = GameObject.FindGameObjectWithTag ("Player");
		enemyAction ();
	}
	
	//Top Level
	//Check for the player and if it is visible
	void targetVisible(){
		//if visible go to inRange
		
		if (Vector3.Distance (transform.position, Player.transform.position) <= visible) {
			//Invoke("InRange",2);
			
			enemyAction = RaiseUp;

			
		}
		//else go to randomNumber
		else {
			enemyAction = idleAnimation;
		}
	}
	
	//Second Level
	//Check if the player is in range for attack
	void inRange(){
		gameObject.rigidbody2D.gravityScale = 0;
		gameObject.rigidbody2D.velocity = new Vector2 (0f, 0f);
		//if in range check for nearDeath
		if (Vector3.Distance (transform.position, Player.transform.position) <= range) {
			enemyAction = Dive;
		}
		//else check if HP not low
		else {
			enemyAction = FlyAround; 
		}
	}
	
	//attack the player
	void  Dive(){
		anim.SetTrigger ("Flight");
		//rotate to player
		transform.LookAt (new Vector2 (Player.transform.position.x, Player.transform.position.y));
		transform.Rotate (new Vector2 (0, -90), Space.Self);
		//transform.Rotate(new Vector3(0,0,180));
		
		//move towards Player
		if (Vector2.Distance (transform.position, Player.transform.position) > 1) {
			transform.Translate (new Vector2 ((speed * 7f) * Time.deltaTime, 0f));
		}
		
	}
	
	//raise up
	void RaiseUp(){
		
		anim.SetTrigger ("Flight");
		//move up
		if ( gameObject.transform.position.y < Player.transform.position.y + 10) {
			//transform.LookAt (new Vector2 (Player.transform.position.x, Player.transform.position.y));
			//transform.Rotate (new Vector2 (0, -90), Space.Self);
			rigidbody2D.velocity += new Vector2(0f,0.5f);
			//transform.Translate (new Vector2 ((speed * 3.5f) * Time.deltaTime, 0f));
			
		}
		else {
			gameObject.rigidbody2D.velocity = new Vector2(0f,0f);
			enemyAction = inRange;
		}
	}
	
	//Fly Around
	void FlyAround(){
		
		gameObject.rigidbody2D.velocity = new Vector2 (0f, 0f);
		gameObject.rigidbody2D.gravityScale = 0;
		if (gameObject.transform.position.x > Player.transform.position.x) {
			transform.Translate (new Vector2 (-(speed * 3.5f) * Time.deltaTime, 0f)); 
		} else {
			transform.Translate (new Vector2 ((speed * 3.5f) * Time.deltaTime, 0f)); 
		}
		if (Vector3.Distance (transform.position, Player.transform.position) <= range ) {
			enemyAction = Dive;
		}
		
		
	}
	
	//wait around
	void idleAnimation(){
		
		//set trigger for idle animation
		anim.SetTrigger ("Idle");
		//reset tree in 3 seconds
		if (!reset) {
			Invoke("resetTree", 3f);
			reset = true;
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		base.OnCollisionEnter2D (coll);
		if (coll.gameObject.tag == "Player") {
			gameObject.transform.rotation = Quaternion.identity;
			resetTree ();
			
		}
		
		//owl death
		if (enemyHealth == 0 && coll.gameObject.tag == "Bullet") {
			
			Destroy (gameObject);
			//audio.Play ();
		}
		//Turn after wall hit
		if (coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Enemy") {
			transform.Rotate(new Vector3(0,0,180));
			
		}
		
	}
	
	void resetTree(){
		enemyAction = targetVisible;
		reset = false;
	}
	
}

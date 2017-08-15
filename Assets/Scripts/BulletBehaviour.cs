using UnityEngine;
using System.Collections;


public class BulletBehaviour : MonoBehaviour, IsDamager<int> {
	
	//the amount of damage the bullet deals
	protected int bulletDamage;
	
	//is the player facing right?
	protected bool isPlayerFacingRight;
	
	//the speed the bullet moves
	protected float movespeed = 25.0f;
	
	//the player GameObject for getting its script
	protected GameObject player;
	
	//the script of the player
	public PlayerControl script;
	
	// Use this for initialization
	void Start() {
		//initialize the damage of the bullet
		this.setDamage(5);
		
		//destroys the bullet 3 seconds after instantiation if it isn’t already
		Destroy(gameObject, 1.5f);
		
		//get the player GameObject
		this.player = GameObject.FindGameObjectWithTag("Player");
		
		//get Player Script
		this.script = player.GetComponent<PlayerControl>();
		
		//if the player is facing right the bullet is facing right
		this.isPlayerFacingRight = script.GetIsFacingRight ();
		
		
	}
	
	public void setIsFacingRight(bool right)
	{
		isPlayerFacingRight = right;
	}
	
	public bool getIsFacingRight()
	{
		return isPlayerFacingRight;
	}
	// Update is called once per frame
	void Update()
	{
		//if player is facing right, the bullet moves right
		if(this.isPlayerFacingRight)
		{
			transform.Translate(movespeed * Time.deltaTime, 0f, 0f);
		}
		//else the player is facing left so the bullet moves left
		else
		{
			transform.Translate(-movespeed * Time.deltaTime, 0f, 0f);
		}
	}
	
	//Trigger is called when the bullet hits something
	void OnTriggerEnter2D(Collider2D col) 
	{
		//the bullet collides with the wall
		if (col.tag == "Wall") 
		{
			//destroy the bullet
			Destroy (gameObject);
		}
		//the bullet collides with an enemy
		else if(col.tag == "Enemy")
		{
			EnemyClass enemyScript = col.GetComponent<EnemyClass>();
			enemyScript.takeDamage(getDamage());
			enemyScript.FlipNeeded(getIsFacingRight());
			if(enemyScript.flashScript == null)
			{
				enemyScript.flashScript = col.transform.GetComponent<FlashInvisible>();
			}
			enemyScript.flashScript.BeenHit = true;
			Destroy(gameObject);
		}
		
	}
	
	
	//change the damage amount of the bullet
	public void setDamage(int damage)
	{
		this.bulletDamage = damage;
	}
	
	//get the amount of damage the bullet deals
	public int getDamage()
	{
		return this.bulletDamage;
	}

	
}
using UnityEngine;
using System.Collections;

public class EnemyClass : MonoBehaviour, IsDamagable<int>, IsDamager<int>, IsKillable {
	/****
		When making a child of EnemyClass.cs make sure you set everything in the Start() function
		of that child class.
	****/
	//health of enemy
	protected int enemyHealth;
	//damage enemy gives out
	protected int enemyDamage;
	//bool to check if the enemy is aware of the player
	protected bool sawPlayer;
	//bool to check if the enemy is facing right
	protected bool facingRight;
	//knockback force is the force applied to the player when hit by an enemy
	//that sends it back
	protected Vector2 knockbackVelocity;
	//reference to the player game object
	public GameObject Player;
	//bool for the death, stops enemy movements/actions
	protected bool dead;
	//a distance threshold where the enemy is aware of the player or changes state
	protected float distThreshold;
	//flag to switch states based on player distance
	protected bool distFlag;
	//bool flag for if enemy is hit then flip the sprite
	protected bool canFlipOnHit;
	//reference to script the deals with sprite flashing when hit
	public FlashInvisible flashScript;

	public AudioClip hitSound;
	
	//gets health of enemy
	public int getHealth()
	{
		return enemyHealth;
	}
	
	//sets enemy health to 'health' variable
	public void setHealth(int health)
	{
		enemyHealth = health;
	}
	
	//sets damage of enemy
	public void setDamage(int damage)
	{
		enemyDamage = damage;
	}
	
	//gets damage of enemy
	public int getDamage()
	{
		return enemyDamage;
	}
	
	//function takes in integer for damage, takes away damageTaken from current health
	public void takeDamage(int damageTaken)
	{
		audio.clip = hitSound;
		audio.Play ();
		enemyHealth -= damageTaken;
	}
	
	//set Knockback Force from a Vector2 argument
	public void setKnockback(Vector2 knockback)
	{
		knockbackVelocity = knockback;
	}
	
	//return a Vector2 for the force, 
	//forceRight bool is for if the force is to the right or to the left
	public Vector2 getKnockback(bool forceRight)
	{
		if (forceRight)
		{
			return knockbackVelocity;
		}
		else
		{
			return knockbackVelocity * -1;
		}
	}
	
	//set the distThreshold, a variable for how far the enemy can see the player
	public void setDistThres(float dist)
	{
		distThreshold = dist;
	}
	
	//return value of distThreshold, value for dist threshold for the enemy has for "line of sight"
	public float getDistThres()
	{
		return distThreshold;
	}
	
	//checks if the player is in the distThreshold 
	public bool isWithinDist()
	{
		if(Vector3.Distance(transform.position, Player.transform.position) < distThreshold )
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	//set the flag for if the player is/isn't within the distThreshold
	public void setDistFlag(bool flag)
	{
		distFlag = flag;
	}
	
	//return the flag for if the player is/isn't within the distThreshold
	public bool getDistFlag()
	{
		return distFlag;
	}
	
	//checks if the player is withing distThreshold and bool isn't true, update flag value
	// or if the player is outside distThreshold and bool isn't false, update flag value
	public virtual void determineDistFlag()
	{
		if (isWithinDist() && !getDistFlag()) 
		{
			setDistFlag(true);
		}
		if(!isWithinDist() && getDistFlag())
		{
			setDistFlag(false);
		}
		
	}
	
	//checks if enemy health is at or below zero
	//returns a true is at or below zero, false otherwise
	public bool isDead()
	{
		return dead || enemyHealth <= 0;
	}
	
	public void Death()
	{
		transform.rigidbody2D.collider2D.isTrigger = true;
		transform.rigidbody2D.gravityScale = 0;
		this.dead = true;
		flashScript.dead = this.dead;
		//stop any force being applied to enemy // to freeze it
		transform.rigidbody2D.velocity = new Vector2 (0, 0);
		//add death animation
		Destroy (gameObject, 0.5f);
	}
	
	//function to flip the sprite if it changes direction
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
	
	public void setCanFlipOnHit(bool flip)
	{
		canFlipOnHit = flip;
	}
	
	public bool getCanFlipOnHit()
	{
		return canFlipOnHit;
	}
	//a basic check for all enemies that have colliders that are isTrigger
	//sets a bool flag for seeing the player, a way for Line of Sight
	void OnTriggerEnter2D(Collider2D collidee)
	{
		if(collidee.gameObject.tag == "Player")
		{
			sawPlayer = true;
		}
	}
	
	//a basic check for all enemies, if the player leaves the isTrigger collider
	//then the enemy has "lost sight of the player"
	void OnTriggerExit2D(Collider2D collidee)
	{
		if(collidee.gameObject.tag == "Player")
		{
			sawPlayer = false;
			
		}
	}
	//deals with conditions for colliding with the player and colliding with a bullet
	protected void OnCollisionEnter2D(Collision2D collider)
	{
		if(collider.transform.tag == "Player")
		{
			PlayerControl playerScript = collider.transform.GetComponent<PlayerControl>();
			playerScript.takeDamage(getDamage());
			if(collider.transform.position.x<transform.position.x)
			{
				//if player is facing right when hit, then send the player to the left
				
				playerScript.KnockBack(new Vector2(-knockbackVelocity.x,knockbackVelocity.y));
			}
			else
			{
				playerScript.KnockBack(new Vector2(knockbackVelocity.x,knockbackVelocity.y));
			}
		}
		
	}
	
	public void FlipNeeded(bool bulletMovingRight)
	{
		if(canFlipOnHit)
		{
			if(facingRight)
			{
				//checks if the bullet was moving in a positive x-direction
				//if facing right and hit by a bullet moving right then flip the sprite to face the shooter
				if(bulletMovingRight)
				{
					Flip();
				}
			}
			else
			{
				//checks if the bullet was moving in a negative x-direction
				//if facing left and hit by a bullet moving left then flip the sprite to face the shooter
				if(!bulletMovingRight)
				{
					Flip();
				}
			}
		}
	}
}

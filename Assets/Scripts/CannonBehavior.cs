using UnityEngine;
using System.Collections;

public class CannonBehavior : EnemyClass {
	public GameObject bullet;
	public GameObject bulletSpawn;
	Animator animator;

	protected int count;
	protected int fireRate;
	protected bool[] pattern;
	protected int currInPattern;
	protected Vector2 bulletVelocity;
	// Use this for initialization
	protected void Start () {
		facingRight = false;
		sawPlayer = false;
		setHealth(10);
		setDamage (5);
		setKnockback (new Vector2 (10, 20));
		count = 0;
		currInPattern = 0;
		animator = GetComponent<Animator> ();
		flashScript = GetComponent<FlashInvisible>();
	}
	
	// Update is called once per frame
	protected void Update () {
		count++;
		if (count == fireRate) 
		{
			count = 0;

			if(pattern[currInPattern])
			{
				Fire();
			}

			if(currInPattern==pattern.Length-1)
			{
				currInPattern = 0;
			}
			else
			{
				currInPattern++;
			}


		}

		if (isDead ()) {
			animator.SetTrigger ("Dead");
			base.Death();
		}
	}

	void Fire()
	{
		animator.SetTrigger("Shoot");
		GameObject r = Instantiate (bullet, bulletSpawn.transform.position, transform.rotation) as GameObject;
		r.rigidbody2D.velocity = bulletVelocity;
	}
	
	public void Death()
	{
		animator.SetTrigger("Dead");
		base.Death ();
	}
}

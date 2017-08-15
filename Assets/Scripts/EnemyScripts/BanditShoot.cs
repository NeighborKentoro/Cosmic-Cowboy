using UnityEngine;
using System.Collections;

public class BanditShoot : EnemyClass{
	
	protected Animator anim;

	float visible;
	public GameObject banditBullet;
	public Transform bulletSpawn;
	float bufferTime;//time for one counter iteration
	float countDown;

	void Start () {
		anim = GetComponent<Animator>();
		facingRight = false;
		sawPlayer = false;
		setDamage(10);
		setHealth(20);
		setKnockback (new Vector2 (10, 20));
		dead = false;
		setDistThres(5);
		setDistFlag(false);
		setCanFlipOnHit (true);
		visible = 20f;

		bufferTime = 0.4f;//Rockets should have a 1sec cooldown
		countDown = Time.time + bufferTime;

		flashScript = GetComponent<FlashInvisible>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead ()) {
			base.Death();
				}
		Player = GameObject.FindGameObjectWithTag ("Player");
		determineDistFlag();

	}

	public override void determineDistFlag()
	{
		float timeLeft = countDown - Time.time;
		if (Vector3.Distance(gameObject.transform.position, Player.gameObject.transform.position) < visible && timeLeft<0)
		{
			Debug.Log("BanditShoot");
			setDistFlag(true);
			anim.SetInteger("Bandit_Anim", 1);
			GameObject Clone;//new Bullet
			Clone = (Instantiate(banditBullet, bulletSpawn.position,transform.rotation)) as GameObject;
			Clone.audio.Play ();//bullet launch sound
			
			//Bullet force
			if(!facingRight){
				Clone.rigidbody2D.AddForce(new Vector2(-700f,0f));
			}
			if(facingRight){
				Clone.rigidbody2D.AddForce(new Vector2(700f,0f));
			}
			//cleanup
			countDown = Time.time + bufferTime;
		}
		if(Vector3.Distance(gameObject.transform.position, Player.gameObject.transform.position) > visible)
		{
			setDistFlag(false);
			anim.SetInteger("Bandit_Anim", 0);
		}
	}
}

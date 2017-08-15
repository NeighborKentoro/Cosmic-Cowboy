using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour, IsDamager<int> {
	public int damage;
	public float lifeSpan;
	public bool passThroughWalls;
	int count;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, lifeSpan);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public int getDamage()
	{
		return damage;
	}
	
	public void setDamage(int d)
	{
		damage = d;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Wall"&&!passThroughWalls)
		{
			Destroy(this.gameObject);
		}
		if (c.gameObject.tag == "Player") 
		{
			PlayerControl playerScript = c.transform.GetComponent<PlayerControl>();

			playerScript.takeDamage(damage);
			if(c.transform.position.x<transform.position.x)
				playerScript.KnockBack(new Vector2(-10,10));
			else
				playerScript.KnockBack(new Vector2(10,10));
			Destroy(this.gameObject);
		}
		

	}
}

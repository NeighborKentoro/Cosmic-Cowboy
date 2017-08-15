using UnityEngine;
using System.Collections;

public class FlashInvisible : MonoBehaviour {

	//reference to the sprite renderer for the sprite this script is attached to
	SpriteRenderer sprite;

	//bool for if the sprite is at its base white color
	bool BaseChanged;
	//counter for when to 'flash' and 'unflash'
	int FlashCount;
	//Color that is white, base color
	Color BaseColor;
	//Color that is white with 0 alpha, makes sprite invisible, used to signify getting hit
	Color InvisColor;
	//Color that is full red, makes sprite a shade of red, used for death anim.
	Color RedColor;
	//do for if dead, if false sprite flashes invisible when, flashes red when dead
	public bool dead;
	//bool for if the enemy has been hit, allows down in Update function to run.
	public bool BeenHit;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		BaseChanged= false;
		FlashCount = 0;
		BeenHit = false;
		BaseColor = new Color (255, 255, 255, 255);
		InvisColor = new Color (255, 255, 255, 0);
		RedColor = new Color (255, 0, 0, 255);
		dead = false;
	}
	
	// Update is called once per frame
	//if the enemy has been hit, set BeenHit to true, code in update will then run
	void LateUpdate () {
		if(BeenHit)
		{
			FlashSprite();
		}
	}
	

	//function to make sprite 'flash' when hit
	//every 5 frames sprite switches from base color to flash color
	//does this alternation for 25 frames
	void FlashSprite()
	{
		
		if(!BaseChanged && FlashCount == 0)
		{
			setToChange();
		}
		if(BaseChanged && FlashCount == 5)
		{
			setToBase();
		}
		if(!BaseChanged && FlashCount == 10)
		{
			setToChange();
		}
		if(BaseChanged && FlashCount == 15)
		{
			setToBase();
		}
		if(!BaseChanged && FlashCount == 20)
		{
			setToChange();
		}
		if(BaseChanged && FlashCount == 25)
		{
			setToBase();
			FlashCount = -1;
			BeenHit = false;
		}
		FlashCount++;
	}

	//changes the sprites color when
	//goes invisible when hit, goes red when hit and is dead
	void setToChange()
	{
		if (dead) 
		{
			sprite.color = InvisColor;
			BaseChanged = true;
		}
		else
		{
			sprite.color = RedColor;
			BaseChanged = true;
		}
	}

	//sets color back to the base color: white
	void setToBase()
	{
		sprite.color = BaseColor;
		BaseChanged = false;
	}
}

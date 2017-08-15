using UnityEngine;
using System.Collections;

public class BodyAnimation : EnemyClass {

	//reference of the Animator Controller of the boss robot body
	protected Animator bodyAnim;

	//bool for if health is low ~ triggered at 1/3 hp or 50 hp
	protected bool isLow;
	//reference for what anim state the body is in.
	protected int roboState;
	//variable for the boss's health, head checks the body component for what it's health should be
	public int prevHealth;

	// Use this for initialization
	public void Start () {
		//get Animator from robot body
		if(bodyAnim = null)
		{
			bodyAnim = GetComponent<Animator>();
		}
		//sets isLow and roboState
		isLow = false;
		roboState = 7;
	}
	//sets the isLow variable and checks if the body Animator is null, if so then get the body Animator
	public void setIsLow(bool value)
	{
		isLow = value;
		if(bodyAnim == null)
		{
			bodyAnim.SetBool ("isLow", isLow);
		}
	}

	//returns the bool variable for isLow
	public bool getIsLow()
	{
		return isLow;
	}

	//sets the variable for what anim state the body is in
	//if body animator is null, then get Animator component
	public void setRoboState(int value)
	{
		roboState = value;
		if(bodyAnim == null)
		{
			bodyAnim = GetComponent<Animator>();
		}
		bodyAnim.SetInteger("roboState", roboState);
	}

	//set the int variable for anim state for the body.
	public int getRoboState()
	{
		return roboState;
	}


}

using UnityEngine;
using System.Collections;

public class starAnimation : MonoBehaviour {

	//the animation number
	private int animNumber;

	// Use this for initialization
	void Start () {
		animNumber = (int) Random.Range (0, 2);
		Animator starAnim = gameObject.GetComponent<Animator> ();
		starAnim.SetInteger("starAnimation", animNumber);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

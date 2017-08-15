using UnityEngine;
using System.Collections;

public class HealthGUI : MonoBehaviour {

	//the player's script for getting player health
	private PlayerControl playerScript;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find ("/CosmicCowboy");
		playerScript = player.GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		GUITexture healthBar = gameObject.GetComponentInChildren<GUITexture> ();
		healthBar.pixelInset = new Rect(healthBar.pixelInset.x, healthBar.pixelInset.y, playerScript.getHealth (), healthBar.pixelInset.height);
		guiText.text = playerScript.getHealth ().ToString ();
	}
}

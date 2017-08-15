using UnityEngine;
using System.Collections;

public class JetPackGUI : MonoBehaviour {

	//the player's script for getting player health
	private PlayerControl playerScript;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find ("/CosmicCowboy");
		playerScript = player.GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		GUITexture cooldownBar = gameObject.GetComponentInChildren<GUITexture> ();
		cooldownBar.pixelInset = new Rect(cooldownBar.pixelInset.x, cooldownBar.pixelInset.y, playerScript.GetJetTime (), cooldownBar.pixelInset.height);
	}
}

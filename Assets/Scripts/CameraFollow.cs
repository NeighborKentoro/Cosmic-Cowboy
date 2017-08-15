using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	//refernce to Player onscreen
	public GameObject player;
	//update the camera's position to the player's current posititon
	void LateUpdate()
	{
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
	}
}

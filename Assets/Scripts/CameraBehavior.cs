using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraBehavior : MonoBehaviour {
	//points for camera to travel between
	//list is for generation of points and array is for travelling between points once finalized
	List<Vector2> points;
	Vector2[] cameraPath;
	int currentIndex;


	public GameObject player;
	// Use this for initialization
	void Start () {
		if (points == null)
			points = new List<Vector2> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (cameraPath == null) 
		{
			Debug.Log("camera path null in camera Behavior");
						return;
		}

		if(player == null) 
		{
			Debug.Log("player null in camera Behavior");
			return;
		}
		if (player.transform.position.x < cameraPath [0].x) 
		{
			// player is probably in starting area
			transform.position = new Vector3 (cameraPath [0].x, cameraPath [0].y, transform.position.z);
		}
		else if(player.transform.position.x>cameraPath[cameraPath.Length-1].x)
		{
			// player is probably in ending area
			transform.position = new Vector3( cameraPath [cameraPath.Length-1].x,cameraPath [cameraPath.Length-1].y,transform.position.z);
		}
		else
		{
			//get correct index
			while(currentIndex < cameraPath.Length-1 && player.transform.position.x > cameraPath[currentIndex+1].x)
				currentIndex++;

			while(currentIndex >= 0 && player.transform.position.x < cameraPath[currentIndex].x)
				currentIndex--;

			//percentage of height difference same as percentage width difference because they're lines
			//set camera height accordingly
			float percentage = (player.transform.position.x - cameraPath[currentIndex].x) / (cameraPath[currentIndex+1].x-cameraPath[currentIndex].x);
			transform.position = new Vector3(player.transform.position.x , cameraPath[currentIndex].y+percentage*(cameraPath[currentIndex+1].y-cameraPath[currentIndex].y), transform.position.z);
		}
	}

	public void Add(Vector2 point)
	{
		if(points == null)
			Debug.Log("points");
		if(point == null)
			Debug.Log("point");
		points.Add (point);
	}
	public void finalizeCameraPath()
	{
		cameraPath = points.ToArray ();
	}

	public Vector2[] getPoints()
	{
		return points.ToArray();
	}

	public void initialize()
	{
		currentIndex = 0;
		points = new List<Vector2> ();
	}
}

using UnityEngine;
using System.Collections;

public class WedgeChunk : MonoBehaviour {
	public static TerrainManager terrainManager;
	public static float cameraHeight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	//random terrain with a sloped platform sticking out of the ground
	public static Vector3 Generate(Vector3 start, float width, float height)
	{
		Vector3 end = RandomTerrain.Generate (start, width);
		GameObject platformInst = Instantiate (terrainManager.platform, new Vector3 ((start.x+end.x)/2, start.y, 0.5f), new Quaternion (0, 0, 0, 0))as GameObject;
		if(Random.Range(0,2)==0)
			platformInst.transform.Rotate (new Vector3(0,0,30));
		else
			platformInst.transform.Rotate (new Vector3(0,0,-30));
		platformInst.transform.localScale = new Vector3(Random.Range(2f,6f),1,1);
		terrainManager.cameraBehavior.Add (new Vector2 (end.x, end.y + cameraHeight));

		terrainManager.generateRandomScenery (new Vector3[] {start,end});

		return end;
	}
}

using UnityEngine;
using System.Collections;

public class PlatformEnemyChunk : MonoBehaviour {
	public static TerrainManager terrainManager;
	public static float cameraHeight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	//same as platform chunk but with an enemy above or below platform
	public static Vector3 Generate(Vector3 start, float width, float height)
	{
		Vector3 end = PlatformChunk.Generate (start, width, height);
		int rand = Random.Range (0, 2);
		if (rand == 0)
			Instantiate (terrainManager.getRandomEnemy (), new Vector3 ((start.x + end.x) / 2, start.y + 5, 0), new Quaternion (0, 0, 0, 0));
		else
			Instantiate (terrainManager.getRandomEnemy (), new Vector3 ((start.x + end.x) / 2, start.y + 15, 0), new Quaternion (0, 0, 0, 0));
	
		terrainManager.generateRandomScenery (new Vector3[] {start,end});

		return end;
	}
}

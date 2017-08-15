using UnityEngine;
using System.Collections;

public class EnemyChunk : MonoBehaviour {
	public static TerrainManager terrainManager;
	public static float cameraHeight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//chunk with enemy in the middle
	public static Vector3 Generate(Vector3 start, float width, float height)
	{
		Vector3 end = FlatTerrain.Generate (start, width, height);
		GameObject platformInst = Instantiate (terrainManager.getRandomEnemy(), new Vector3 ((start.x + end.x)/2, start.y + 5, 0), new Quaternion (0, 0, 0, 0))as GameObject;

		terrainManager.cameraBehavior.Add (new Vector2 (end.x, end.y + cameraHeight));

		terrainManager.generateRandomScenery (new Vector3[] {start,end});
		return end;
	}
}

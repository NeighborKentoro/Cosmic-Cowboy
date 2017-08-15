using UnityEngine;
using System.Collections;

public class FlatTerrainChunk : MonoBehaviour {
	public static TerrainManager terrainManager;
	public static float cameraHeight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//just flat terrain
	public static Vector3 Generate(Vector3 start, float width, float height)
	{
		Vector2 point = FlatTerrain.Generate (start,width,height);
		terrainManager.cameraBehavior.Add (new Vector2 (point.x, point.y + cameraHeight));
		terrainManager.generateRandomScenery (new Vector3[] {start,point});
		return point;
	}
}

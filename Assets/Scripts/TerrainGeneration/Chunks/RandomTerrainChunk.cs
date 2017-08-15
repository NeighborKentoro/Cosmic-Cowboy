using UnityEngine;
using System.Collections;

public class RandomTerrainChunk : MonoBehaviour {
	public static TerrainManager terrainManager;
	public static float cameraHeight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//generate a chunk with random bumpy terrain
	public static Vector3 Generate(Vector3 start, float segmentWidth, float segmentHeight, int numSegments)
	{
		Vector2 point = RandomTerrain.Generate (start, segmentWidth, segmentHeight, numSegments);
		terrainManager.cameraBehavior.Add (new Vector2 (point.x, point.y + cameraHeight));
		return point;
	}

	public static Vector3 Generate(Vector3 start, float segmentWidth, float segmentHeight)
	{
		return Generate (start, segmentWidth/5.0f, segmentHeight, 5);
	}
}

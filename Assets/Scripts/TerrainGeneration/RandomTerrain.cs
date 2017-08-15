using UnityEngine;
using System.Collections;

public class RandomTerrain : MonoBehaviour {
	//prefab for the terrain object
	static public TerrainManager terrainManager;
	static float DEFAULT_MIN_CHANGE = -2.0F;
	static float DEFAULT_MAX_CHANGE = 2.0F;
	static float DEFAULT_HEIGHT = 20.0F;

	void Start () {
	
	}
	

	void Update () {
	
	}
	/*
	 * 		generate a random piece of terrain. Every 2 vertices are one on top of the other. The space between the pairs of vertices are segments
	 * 		minChange is the bottom of the range of change in y of vertice pairs, and maxChange is the top of that range
	 */
	public static Vector3 Generate(Vector3 start, float segmentWidth, float segmentHeight, int numSegments, float minChange,float maxChange)
	{
		int vertNum = numSegments * 2 + 2;
		Vector3[] vertices = new Vector3[vertNum];

		vertices[0] = start;
		vertices[1] = new Vector3 (start.x,start.y-segmentHeight,start.z);

		for(int i = 2; i<vertNum; i+=2){

			Vector3 last = vertices[i-2];
			float change = Random.Range(minChange,maxChange);
			//initialize the next segment
			vertices[i] = new Vector3 (last.x+segmentWidth,last.y+change,last.z);
			vertices[i+1] = new Vector3 (last.x+segmentWidth,last.y-segmentHeight+change,last.z);
		}
		
		GameObject piece1 = Instantiate (terrainManager.terrainPiece, new Vector3(0,0,0), new Quaternion(0,0,0,0))as GameObject;
		Terrain t1 = piece1.GetComponent<Terrain>();
		t1.vertices = vertices;
		return vertices [vertices.Length - 2];
	}

	/*
	 * 		generate a random piece of terrain. Every 2 vertices are one on top of the other. The space between the pairs of vertices are segments
	 */
	public static Vector3 Generate(Vector3 start, float segmentWidth, float segmentHeight, int numSegments)
	{
		return Generate (start, segmentWidth, segmentHeight, numSegments, DEFAULT_MIN_CHANGE, DEFAULT_MAX_CHANGE);
	}

	/*
	 * 		generate a random piece of terrain. Every 2 vertices are one on top of the other. The space between the pairs of vertices are segments
	 */
	public static Vector3 Generate(Vector3 start, float segmentWidth, int numSegments)
	{
		return Generate (start, segmentWidth, DEFAULT_HEIGHT, numSegments, DEFAULT_MIN_CHANGE, DEFAULT_MAX_CHANGE);
	}

	/*
	 * 		generate a random piece of terrain with the given width
	 */
	public static Vector3 Generate(Vector3 start, float width)
	{
		return Generate (start, width/10, DEFAULT_HEIGHT, 10, DEFAULT_MIN_CHANGE, DEFAULT_MAX_CHANGE);
	}

}

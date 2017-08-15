using UnityEngine;
using System.Collections;

public class FlatTerrain : MonoBehaviour {
	static public TerrainManager terrainManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public static Vector3 Generate(Vector3 start, float width, float height, float heightChange)
	{
		Vector3[] vertices = new Vector3[4];
		//initialize all of the vertices
		//first initialize the first 2 vertices as a starting point
		vertices[0] = start;
		vertices[1] = new Vector3 (start.x,start.y-height,start.z);
		vertices[2] = new Vector3 (start.x+width,start.y+heightChange,start.z);
		vertices[3] = new Vector3 (start.x+width,start.y-height,start.z);
		
		GameObject piece1 = Instantiate (terrainManager.terrainPiece, new Vector3(0,0,0), new Quaternion(0,0,0,0))as GameObject;
		Terrain t1 = piece1.GetComponent<Terrain>();
		t1.vertices = vertices;

		return vertices [vertices.Length - 2];
	}

	public static Vector3 Generate(Vector3 start, float width, float height)
	{
		return Generate(start, width, height, 0);
	}
}

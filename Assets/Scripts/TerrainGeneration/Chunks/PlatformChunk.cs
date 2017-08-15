using UnityEngine;
using System.Collections;

public class PlatformChunk : MonoBehaviour {
	public static TerrainManager terrainManager;
	public static float cameraHeight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//sloped ground with platform of same slope
	public static Vector3 Generate(Vector3 start, float width, float height)
	{
		Vector3[] vertices = new Vector3[4];
		//initialize all of the vertices
		//first initialize the first 2 vertices as a starting point
		float change = Random.Range (-2.0F, 2.0F);
		vertices[0] = start;
		vertices[1] = new Vector3 (start.x,start.y-height,start.z);
		vertices[2] = new Vector3 (start.x+width,start.y+change,start.z);
		vertices[3] = new Vector3 (start.x+width,start.y-height+change,start.z);
		
		GameObject piece1 = Instantiate (terrainManager.terrainPiece, new Vector3(0,0,0), new Quaternion(0,0,0,0))as GameObject;
		GameObject platformInst = Instantiate (terrainManager.platform, new Vector3(start.x+width/2.0F,start.y+11,0), new Quaternion(0,0,0,0))as GameObject;
		platformInst.transform.Rotate (new Vector3 (0, 0, Mathf.Atan2(change,width)*(180.0F/3.14159F)));
		float resize = Random.Range (0.0F, 4.0F);
		platformInst.transform.localScale += new Vector3 (resize, 0, 0);
		Terrain t1 = piece1.GetComponent<Terrain>();
		t1.vertices = vertices;
		terrainManager.cameraBehavior.Add (new Vector2 (vertices [vertices.Length - 2].x, vertices [vertices.Length - 2].y + cameraHeight));

		terrainManager.generateRandomScenery (new Vector3[] {vertices[0],vertices[2]});


		return vertices [vertices.Length - 2];
	}
}

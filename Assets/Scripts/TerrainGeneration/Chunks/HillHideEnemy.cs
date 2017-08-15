using UnityEngine;
using System.Collections;

public class HillHideEnemy : MonoBehaviour {
	public static TerrainManager terrainManager;
	public static float cameraHeight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//enemy hiding under platform with hill to the right
	public static Vector3 Generate(Vector3 start, float width, float height)
	{
		Vector3[] vertices = new Vector3[10];
		//initialize all of the vertices
		//first initialize the first 2 vertices as a starting point
		float change = Random.Range (0, 1.0F);
		vertices[0] = start;
		vertices[1] = new Vector3 (start.x,start.y-height,start.z);
		vertices[2] = new Vector3 (start.x+width*0.75f/2f,start.y+change,start.z);
		vertices[3] = new Vector3 (start.x+width*0.75f/2f,start.y-height+change,start.z);
		vertices[4] = new Vector3 (start.x+width*1f/2f,start.y+11,start.z);
		vertices[5] = new Vector3 (start.x+width*1f/2f,start.y-height+11,start.z);
		vertices[6] = new Vector3 (start.x+width*3f/4f,start.y+7,start.z);
		vertices[7] = new Vector3 (start.x+width*3f/4f,start.y-height+7,start.z);
		vertices[8] = new Vector3 (start.x+width,start.y,start.z);
		vertices[9] = new Vector3 (start.x+width,start.y-height,start.z);
		
		GameObject piece1 = Instantiate (terrainManager.terrainPiece, new Vector3(0,0,0), new Quaternion(0,0,0,0))as GameObject;
		GameObject platformInst = Instantiate (terrainManager.platform, new Vector3(start.x+width*0.5f/2.0F,start.y+11,0), new Quaternion(0,0,0,0))as GameObject;
		float resize = Random.Range (0.0F, 3.0F);
		platformInst.transform.localScale += new Vector3 (resize, 0, 0);
		Instantiate (terrainManager.getRandomEnemy (), new Vector3 (start.x + width*0.5f/2.0f, start.y + 4f, start.z),new Quaternion(0,0,0,0));
		Terrain t1 = piece1.GetComponent<Terrain>();
		t1.vertices = vertices;
		terrainManager.cameraBehavior.Add (new Vector2 (vertices [vertices.Length - 2].x, vertices [vertices.Length - 2].y + cameraHeight));


		terrainManager.generateRandomScenery (new Vector3[] {vertices[0],vertices[8]});

		return vertices [vertices.Length - 2];
	}
}

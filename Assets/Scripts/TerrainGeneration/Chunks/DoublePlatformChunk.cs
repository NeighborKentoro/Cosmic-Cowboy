using UnityEngine;
using System.Collections;

public class DoublePlatformChunk : MonoBehaviour {
		public static TerrainManager terrainManager;
		public static float cameraHeight;
		
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		
		//randomly sloped terrain with platform with same slope
		//1 enemy on platform 1 below
		public static Vector3 Generate(Vector3 start, float width, float height)
		{
			Vector3 end = FlatTerrain.Generate (start, width, height,Random.Range(0,3));
			
			GameObject platformInst = Instantiate (terrainManager.platform, new Vector3(start.x+width/2.0F,start.y+11,0), new Quaternion(0,0,0,0))as GameObject;
			float resize = Random.Range (0.0F, 4.0F);
			platformInst.transform.localScale += new Vector3 (resize, 0, 0);

			platformInst = Instantiate (terrainManager.platform, new Vector3(start.x+width/2.0F,start.y+22,0), new Quaternion(0,0,0,0))as GameObject;
			resize = Random.Range (0.0F, 4.0F);
			platformInst.transform.localScale += new Vector3 (resize, 0, 0);

			
			terrainManager.cameraBehavior.Add (new Vector2 (end.x, end.y + cameraHeight));
			
			terrainManager.generateRandomScenery (new Vector3[] {start,end});
			
			return end;
		}
	}


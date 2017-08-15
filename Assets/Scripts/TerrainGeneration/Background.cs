using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
		public Vector3[] vertices;
		
		public Material material;
		
		int vertNum;
		int triNum;
		
		public float resizeFactor = 1.0F;
		// Use this for initialization
		void Start ()
		{
			vertNum = vertices.Length;
			triNum = vertNum-2;
			
			int[] triangles = new int[triNum*3];
			Vector2[] uv = new Vector2[vertNum];
			
			MeshFilter meshFilter = GetComponent<MeshFilter> ();
			if (meshFilter == null) {
				Debug.LogError ("MeshFilter not found!");
				return;
			}
			
			
			//clone the mesh vertices for the uv for texture mapping
			
			for(int i=0;i<vertices.Length;i = i+2){
				//uv[i] = new Vector2(vertices[i].x/resizeFactor,1);
				uv[i] = new Vector2(vertices[i].x/resizeFactor,vertices[i].y);
				uv[i+1] = new Vector2(vertices[i].x/resizeFactor,vertices[i+1].y);
			//uv[i+1] = new Vector2(vertices[i].x/resizeFactor,1-((vertices[i].y-vertices[i+1].y)/resizeFactor));
			}
			
			//initialize the triangles
			for(int i = 0; i<triNum*3;i+=6){
				int a;
				if(i>0)
					a = i/3;
				else
					a = 0;
				int b = a+1;
				int c = b+1;
				int d = c+1;
				triangles[i] = c;
				triangles[i+1] = b;
				triangles[i+2] = a;
				triangles[i+3] = b;
				triangles[i+4] = c;
				triangles[i+5] = d;
			}
			
			//initialize the mesh object
			Mesh mesh = meshFilter.sharedMesh;
			if (mesh == null) {
				meshFilter.mesh = new Mesh ();
				mesh = meshFilter.sharedMesh;
			}
			mesh.vertices = vertices;
			mesh.triangles = triangles;
			mesh.RecalculateNormals ();
			
			mesh.Optimize ();
			
			mesh.uv = uv;
			
			MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
			meshRenderer.material = material;
			transform.localScale = new Vector3 (1, 3, 1);
		}
		void Update ()
		{
			
			
			
		}
		
		
		
	}

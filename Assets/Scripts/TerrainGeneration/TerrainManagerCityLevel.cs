using UnityEngine;
using System.Collections;

public class TerrainManagerCityLevel : TerrainManager {

	// Use this for initialization
	void Start () {
		base.setEnemies ();
		setChunks ();
		setScenery ();
		cameraBehavior = camera.GetComponent<CameraBehavior> ();
		cameraBehavior.initialize ();
		base.InitializeChunks ();
		
		//set up starting area
		base.last = new Vector3 (transform.position.x+30,transform.position.y-10,transform.position.z);
		FlatTerrain.Generate (new Vector3 (last.x-80, last.y, last.z),80f,20f);
		cameraBehavior.Add (new Vector2 (last.x-30, last.y + cameraHeight));
		
		//generate the level
		for (int i = 0; i<10; i++) 
		{
			Vector3 temp;
			ChunkOdds.Generate current = getRandomChunk();
			temp = current (last,20.0F,50.0F);
			last = temp;
		}
		
		for (int i = 0; i< hostileChunks.Length; i++)
			hostileChunks [i].weight += 15;
		
		for (int i = 0; i< multiEnemyChunks.Length; i++)
			multiEnemyChunks [i].weight += 5;
		//generate the level
		for (int i = 0; i<10; i++) 
		{
			Vector3 temp;
			ChunkOdds.Generate current = getRandomChunk();
			temp = current (last,20.0F,50.0F);
			last = temp;
		}
		
		for (int i = 0; i< hostileChunks.Length; i++)
			hostileChunks [i].weight += 40;
		
		for (int i = 0; i< multiEnemyChunks.Length; i++)
			multiEnemyChunks [i].weight += 15;
		//generate the level
		for (int i = 0; i<10; i++) 
		{
			Vector3 temp;
			ChunkOdds.Generate current = getRandomChunk();
			temp = current (last,20.0F,50.0F);
			last = temp;
		}
		
		//set up ending area
		FlatTerrain.Generate (last,50,20);

		//create end of level trigger
		//create a new vector3 for the sake of aligning the end of level trigger better
		Vector3 endVector = new Vector3 (last.x + 30, last.y + 20, last.z);
		Object instance = Instantiate(Resources.Load("EndOfLevel", typeof(GameObject)), endVector, transform.rotation);
		
		cameraBehavior.finalizeCameraPath ();
		base.createSimpleBackground ();
		//Instantiate (player, new Vector3(6,-7,0), new Quaternion(0,0,0,0))as GameObject;
		//player is prefab
	}


	void setChunks()
	{
				chunks = new ChunkOdds[]{
			//new ChunkOdds (10, RandomTerrainChunk.Generate),
			new ChunkOdds (10, FlatTerrainChunk.Generate),
			new ChunkOdds (10, PlatformChunk.Generate),
			//new ChunkOdds (10, WedgeChunk.Generate),
			new ChunkOdds (10, HighPlatformChunk.Generate),
			new ChunkOdds (10, DoublePlatformChunk.Generate),
			new ChunkOdds (20, EnemyChunk.Generate),
			new ChunkOdds (10, PlatformEnemyChunk.Generate),
			new ChunkOdds (0, PlatformEnemiesChunk.Generate),
			//new ChunkOdds (10, HillHideEnemy.Generate),
			new ChunkOdds (10, HighPlatformEnemiesChunk.Generate),
			new ChunkOdds (20, DoublePlatformEnemyChunk.Generate),
			new ChunkOdds (10, DoublePlatformEnemiesChunk.Generate)
		};

		passiveChunks = new ChunkOdds[]{chunks [0],chunks [1],chunks [2],chunks[3]};
		hostileChunks = new ChunkOdds[]{chunks[4], chunks[5], chunks[6],chunks [7],chunks [8],chunks [9]};
		multiEnemyChunks = new ChunkOdds[]{chunks [6],chunks [7],chunks [9]};
	}

	protected void setScenery()
	{
		
		mountain.transform.localScale = new Vector3 (3.5f, 3.5f, 1);
		scenery = new SceneryOdds[]{
			new SceneryOdds (10, smallCactus,0.6f,-1f),
			new SceneryOdds (10, largeCactus,2.7f,-1f),
			//new SceneryOdds (10, tree,5f,-1f),
			new SceneryOdds (10, smallCactus,0.6f,1f),
			new SceneryOdds (10, largeCactus,2f,1f),
			new SceneryOdds (10, tree,4.5f,1f),
			new SceneryOdds (10, mountain,0f,39)
		};
		
		backgroundScenery = new SceneryOdds[]{scenery[2], scenery[3],scenery[4],scenery[5]};
		foregroundScenery = new SceneryOdds[]{scenery[0],scenery[1]};
	}
}

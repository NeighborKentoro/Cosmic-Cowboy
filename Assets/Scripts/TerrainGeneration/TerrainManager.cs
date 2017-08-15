using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour {


	public GameObject terrainPiece;
	public GameObject backgroundPiece;
	public GameObject platform;
	public GameObject snake;
	public GameObject buffalo;
	public GameObject cannonLeft;
	public GameObject robotOwl;
	public GameObject banditPoncho;
	public GameObject altCannonDiagonal;
	public GameObject altCannonLeft;

	public GameObject smallCactus;
	public GameObject largeCactus;
	public GameObject tree;
	public GameObject mountain;

	public GameObject camera;
	public CameraBehavior cameraBehavior;
	public float cameraDepth = 100f;
	public float cameraSize = 20f;
	protected Vector3 last;

	public float cameraHeight = 20f;


	EnemyOdds[] enemies;
	protected SceneryOdds[] scenery;
	protected SceneryOdds[] backgroundScenery;
	protected SceneryOdds[] foregroundScenery;
	protected ChunkOdds[] chunks;
	protected ChunkOdds[] passiveChunks;
	protected ChunkOdds[] hostileChunks;	
	protected ChunkOdds[] multiEnemyChunks;
	// Use this for initialization
	protected void Start () {
		setEnemies ();
		setChunks ();
		setScenery ();
		cameraBehavior = camera.GetComponent<CameraBehavior> ();
		cameraBehavior.initialize ();
		InitializeChunks ();

		//set up starting area
		last = new Vector3 (transform.position.x+30,transform.position.y-10,transform.position.z);
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
		createSimpleBackground ();
		//Instantiate (player, new Vector3(6,-7,0), new Quaternion(0,0,0,0))as GameObject;
		//player is prefab
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void InitializeChunks()
	{
		RandomTerrain.terrainManager = this;
		FlatTerrain.terrainManager = this;

		RandomTerrainChunk.terrainManager = this;
		RandomTerrainChunk.cameraHeight = cameraHeight;

		FlatTerrainChunk.terrainManager = this;
		FlatTerrainChunk.cameraHeight = cameraHeight;

		PlatformChunk.terrainManager = this;
		PlatformChunk.cameraHeight = cameraHeight;

		EnemyChunk.terrainManager = this;
		EnemyChunk.cameraHeight = cameraHeight;

		PlatformEnemiesChunk.terrainManager = this;
		PlatformEnemiesChunk.cameraHeight = cameraHeight;

		WedgeChunk.terrainManager = this;
		WedgeChunk.cameraHeight = cameraHeight;

		PlatformEnemyChunk.terrainManager = this;
		PlatformEnemyChunk.cameraHeight = cameraHeight;

		HillHideEnemy.terrainManager = this;
		HillHideEnemy.cameraHeight = cameraHeight;

		HighPlatformChunk.terrainManager = this;
		HighPlatformChunk.cameraHeight = cameraHeight;

		HighPlatformEnemiesChunk.terrainManager = this;
		HighPlatformEnemiesChunk.cameraHeight = cameraHeight;

		DoublePlatformChunk.terrainManager = this;
		DoublePlatformChunk.cameraHeight = cameraHeight;

		DoublePlatformEnemyChunk.terrainManager = this;
		DoublePlatformEnemyChunk.cameraHeight = cameraHeight;

		DoublePlatformEnemiesChunk.terrainManager = this;
		DoublePlatformEnemiesChunk.cameraHeight = cameraHeight;
	}

	/*
	 * creates background by creating meshes between adjacent camera points
	 */
	protected void createBackground()
	{
		Vector2[] cameraPoints = cameraBehavior.getPoints ();
		Vector3[] backGroundVertices = new Vector3[4];

		backGroundVertices [0] = new Vector3 (cameraPoints[0].x,cameraPoints[0].y+cameraSize,cameraDepth);
		backGroundVertices [1] = new Vector3 (cameraPoints[0].x,cameraPoints[0].y-cameraSize,cameraDepth);
		for (int i = 1; i < cameraPoints.Length; i++) 
		{
			backGroundVertices [2] = new Vector3 (cameraPoints[i].x,cameraPoints[i].y+cameraSize,cameraDepth);
			backGroundVertices[3] = new Vector3 (cameraPoints[i].x,cameraPoints[i].y-cameraSize,cameraDepth);

			Vector3[] temp = new Vector3[4];

			for(int j = 0;j<4;j++)
				temp[j] = backGroundVertices[j];

			GameObject piece2 = Instantiate (backgroundPiece, new Vector3(0,0,cameraDepth), new Quaternion(0,0,0,0))as GameObject;

			Background b1 = piece2.GetComponent<Background>();
			b1.vertices = temp;

			backGroundVertices [0] = new Vector3(backGroundVertices[2].x,backGroundVertices[2].y,backGroundVertices[2].z);
			backGroundVertices[1] = new Vector3(backGroundVertices[3].x,backGroundVertices[3].y,backGroundVertices[3].z);

		}

	}

	/*
	 * create background by creating background with start relative to first camera point
	 * and end relative to last camera point
	 */
	protected void createSimpleBackground()
	{
		Vector2[] cameraPoints = cameraBehavior.getPoints ();
		Vector3[] backGroundVertices = new Vector3[4];

		backGroundVertices [0] = new Vector3 (cameraPoints[0].x-300,cameraPoints[0].y+cameraSize,cameraDepth);
		backGroundVertices [1] = new Vector3 (cameraPoints[0].x-300,cameraPoints[0].y-cameraSize,cameraDepth);
		backGroundVertices [2] = new Vector3 (cameraPoints[cameraPoints.Length-1].x+300,cameraPoints[cameraPoints.Length-1].y+cameraSize,cameraDepth);
		backGroundVertices[3] = new Vector3 (cameraPoints[cameraPoints.Length-1].x+300,cameraPoints[cameraPoints.Length-1].y-cameraSize,cameraDepth);
			
		GameObject piece2 = Instantiate (backgroundPiece, new Vector3(0,0,cameraDepth), new Quaternion(0,0,0,0))as GameObject;
			
		Background b1 = piece2.GetComponent<Background>();
		b1.vertices = backGroundVertices;
	}

	public GameObject getRandomEnemy()
	{
		GameObject ret = null;
		int max = 0;
		for (int i = 0; i<enemies.Length; i++)
						max = max + enemies [i].weight;
		int rand = Random.Range (0, max);

		int currentIndex = 0;
		int currentWeight = enemies [currentIndex].weight;

		while (ret==null) 
		{
			if (rand > currentWeight) 
			{
				currentIndex++;
				currentWeight = currentWeight + enemies [currentIndex].weight;
			}
			else
			{
				// enemy selected
				ret = enemies[currentIndex].enemy;

				//adjust weights accordingly
				enemies[currentIndex].weight -= 5;
				for(int i = 0;i<enemies.Length; i++)
				{
					if(i!= currentIndex)
						enemies[i].weight++;
				}
			}
		}
		return ret;
	}

	protected ChunkOdds.Generate getRandomChunk()
	{
		ChunkOdds.Generate ret = null;
		int max = 0;
		for (int i = 0; i<chunks.Length; i++)
			max = max + chunks [i].weight;
		int rand = Random.Range (0, max);
		
		int currentIndex = 0;
		int currentWeight = chunks [currentIndex].weight;
		
		while (ret==null) 
		{
			if (rand > currentWeight) 
			{
				currentIndex++;
				currentWeight = currentWeight + chunks [currentIndex].weight;
			}
			else
			{
				//chunk selected
				ret = chunks[currentIndex].generate;

				//adjust weights accordingly
				chunks[currentIndex].weight -= 5;
				for(int i = 0;i<enemies.Length; i++)
				{
					if(i!= currentIndex)
						chunks[i].weight++;
				}

				// if hostile chunk selected increase odds of passive chunk
				// and vice versa
				if(isChunkHostile(chunks[currentIndex].generate))
				{
					for(int i = 0;i<passiveChunks.Length;i++)
					{
						passiveChunks[i].weight += 2;
					}

					for(int i = 0;i<hostileChunks.Length;i++)
					{
						hostileChunks[i].weight -= 2;
					}
				}
				else
				{
					for(int i = 0;i<passiveChunks.Length;i++)
					{
						passiveChunks[i].weight -= 2;
					}
					
					for(int i = 0;i<hostileChunks.Length;i++)
					{
						hostileChunks[i].weight += 2;
					}
				}

			}
		}
		return ret;
	}

	protected void setEnemies()
	{
		enemies = new EnemyOdds[] {
			new EnemyOdds (10, snake),
			new EnemyOdds (10, cannonLeft),
			new EnemyOdds (10, buffalo),
			new EnemyOdds (10, robotOwl),
			new EnemyOdds (10, banditPoncho),
			new EnemyOdds (10, altCannonDiagonal),
			new EnemyOdds (10, altCannonLeft)
		};
	}

	protected void setChunks()
	{
		chunks = new ChunkOdds[]{
			new ChunkOdds(10,RandomTerrainChunk.Generate),
			new ChunkOdds(10,FlatTerrainChunk.Generate),
			new ChunkOdds(10,PlatformChunk.Generate),
			new ChunkOdds(10,WedgeChunk.Generate),
			new ChunkOdds(10,HighPlatformChunk.Generate),
			new ChunkOdds(10,DoublePlatformChunk.Generate),
			new ChunkOdds(10,EnemyChunk.Generate),
			new ChunkOdds(10,PlatformEnemyChunk.Generate),
			new ChunkOdds(0,PlatformEnemiesChunk.Generate),
			new ChunkOdds(10,HillHideEnemy.Generate),
			new ChunkOdds(0,HighPlatformEnemiesChunk.Generate),
			new ChunkOdds(10,DoublePlatformEnemyChunk.Generate),
			new ChunkOdds(0,DoublePlatformEnemiesChunk.Generate)
		};

		passiveChunks = new ChunkOdds[]{chunks [0],chunks [1],chunks [2],chunks[3],chunks [4],chunks [5]};
		hostileChunks = new ChunkOdds[]{chunks[6],chunks [7],chunks [8],chunks [9],chunks [10],chunks [11],chunks [12]};
		multiEnemyChunks = new ChunkOdds[]{chunks [8],chunks [10],chunks [12]};
	}

	protected void setScenery()
	{

		mountain.transform.localScale = new Vector3 (3.5f, 3.5f, 1);
		scenery = new SceneryOdds[]{
			new SceneryOdds (10, smallCactus,0.6f,-1f),
			new SceneryOdds (10, largeCactus,2.7f,-1f),
			new SceneryOdds (10, tree,5f,-1f),
			new SceneryOdds (10, smallCactus,0.6f,1f),
			new SceneryOdds (10, largeCactus,2f,1f),
			new SceneryOdds (10, tree,4.5f,1f),
			new SceneryOdds (10, mountain,0f,39)
		};

		backgroundScenery = new SceneryOdds[]{scenery[3],scenery[4],scenery[5],scenery[6]};
		foregroundScenery = new SceneryOdds[]{scenery[0],scenery[1],scenery[2]};
	}

	protected bool isChunkHostile(ChunkOdds.Generate chunk)
	{
		for (int i =0; i< hostileChunks.Length; i++) {
			if(chunk == hostileChunks[i].generate)
				return true;
		}
		return false;
	}

	protected bool isBackgroundScenery(SceneryOdds item)
	{
		for (int i =0; i< backgroundScenery.Length; i++) {
			if(item.Equals(backgroundScenery[i]))
				return true;
		}
		return false;
	}

	public void generateRandomScenery(Vector3[] points)
	{
		SceneryOdds ret = null;
		int max = 0;
		for (int i = 0; i<scenery.Length; i++)
			max = max + scenery [i].weight;
		int rand = Random.Range (0, max);
		
		int currentIndex = 0;
		int currentWeight = scenery [currentIndex].weight;
		
		while (ret==null) 
		{
			if (rand > currentWeight) 
			{
				currentIndex++;
				currentWeight = currentWeight + scenery [currentIndex].weight;
			}
			else
			{
				// enemy selected
				ret = scenery[currentIndex];
				
				//adjust weights accordingly
				scenery[currentIndex].weight -= 5;
				for(int i = 0;i<scenery.Length; i++)
				{
					if(i!= currentIndex)
						scenery[i].weight++;
				}
			}
		}



		float sceneryX = Random.Range (points[0].x, points[points.Length-1].x);
		int currXIndex = 0;
		while (currXIndex<points.Length-1&&sceneryX>points[currXIndex+1].x) 
		{
			currXIndex++;
		}
		float percentage = (sceneryX - points[currXIndex].x) / (points[currXIndex+1].x-points[currXIndex].x);
		Instantiate (ret.enemy, new Vector3(sceneryX,points[currXIndex].y+percentage*(points[currXIndex+1].y-points[currXIndex].y)+ret.heightOffset,ret.depth), new Quaternion(0,0,0,0));

	}
	
}

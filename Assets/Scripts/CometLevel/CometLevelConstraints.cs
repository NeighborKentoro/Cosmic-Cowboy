using UnityEngine;
using System.Collections;

//this class is used for holding the constraint values for generating the comet level
public class CometLevelConstraints {

	//the maximum distance the player can fly
	private float maxPlayerFlightDistance = 70f;

	//the maximum x length of a comet
	private float maxCometXLength = 22f;

	//the maximum y length of a comet
	private float maxCometYLength = 23f;

	//the length of the level
	private float levelLength = 450;

	//the maximum height the comet can be generated at
	private float maxCometYSpawnHeight = 24;

	//the minimum height the comet can be generated at
	private float minCometYSpawnHeight = -25;

	//the probability that a comet will spawn, 20%
	private int cometProbability = 2;

	//the probability that a meteorite spawner will spawn
	private int meteoriteSpawnerProbability = 1;

	//the minimum distance that meteorite spawners must not spawn
	private float minMeteoriteSpawnDistance = 75;

	//the maximum distance before a meteorite spawner is forced to spawn
	private float maxMeteoriteSpawnDistance = 100;

	
	//returns the maxPlayerFlightDistance
	public float MaxPlayerFlightDistance () {
		return this.maxPlayerFlightDistance;
	}

	//returns the maxCometXLength
	public float MaxCometXLength () {
		return this.maxCometXLength;
	}

	//returns the maxCometYLength
	public float MaxCometYLength () {
		return this.maxCometYLength;
	}

	//returns the maxCometYSpawnHeight
	public float MaxCometYSpawnHeight () {
		return this.maxCometYSpawnHeight;
	}

	//returns the minCometYSpawnHeight
	public float MinCometYSpawnHeight () {
		return this.minCometYSpawnHeight;
	}

	//returns the levelLength
	public float LevelLength () {
		return this.levelLength;
	}

	//returns the cometProbability
	public int CometProbability () {
		return this.cometProbability;
	}

	//returns the meteoriteSpawnerProbability
	public int MeteoriteSpawnerProbability () {
		return this.meteoriteSpawnerProbability;
	}

	//returns the minMeteoriteSpawnDistance
	public float MinMeteoriteSpawnDistance () {
		return this.minMeteoriteSpawnDistance;
	}

	//returns the maxMeteoriteSpawnDistance
	public float MaxMeteoriteSpawnDistance () {
		return this.maxMeteoriteSpawnDistance;
	}
}

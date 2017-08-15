using UnityEngine;
using System.Collections;

public class SceneryOdds {
	public int weight;
	public GameObject enemy;
	public float heightOffset;
	public float depth;

	public SceneryOdds(int inWeight, GameObject inEnemy,float inHeightOffset,float indepth)
	{
		weight = inWeight;
		enemy = inEnemy;
		heightOffset = inHeightOffset;
		depth = indepth;
	}

}
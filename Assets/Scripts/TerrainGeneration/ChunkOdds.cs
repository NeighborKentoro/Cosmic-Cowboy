using UnityEngine;
using System.Collections;

public class ChunkOdds {
	public delegate Vector3 Generate(Vector3 point,float width, float height);
	public int weight;
	public Generate generate;
	public ChunkOdds(int inWeight,Generate inGenerate)
	{
		weight = inWeight;
		generate = inGenerate;
	}
}

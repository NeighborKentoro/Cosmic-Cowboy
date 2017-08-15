using UnityEngine;
using System.Collections;

//this class procedurally generates the comet level
public class ProceduralGenerationCometLevel : MonoBehaviour {

	//the constraints for the level
	private CometLevelConstraints levelConstraints;

	//the current position through the pass
	private float passPosition = 0;

	//the position of the last spawn
	private float lastSpawnPosition = 0;

	//the rigidbody of the comet
	private Rigidbody2D comet;

	//the rigidbody of the meteorite spawnpoint
	private Rigidbody2D meteoriteSpawn;


	void Awake () {
		//initialze the prefabs for comet and meteoriteSpawn
		this.comet = GameObject.Find ("/Comet").rigidbody2D;
		//this.meteoriteSpawn = GameObject.Find ("/MeteoriteSpawn").rigidbody2D;

		//these are called in the awake so that they are done before everything else
		//initialize the level constraints
		this.levelConstraints = new CometLevelConstraints();

		//generate the level
		this.GenerateLevel ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//This calls all the passes to generate the level
	private void GenerateLevel () {
		//call the first pass
		this.Pass1 ();

		//call the second pass
		this.Pass2 ();

		//call the final pass
		this.FinalPass ();
	}

	//the first pass for generating the level
	//this pass generates the comets
	private void Pass1 () {

		//while we have not reached the end of the level
		while (this.passPosition < this.levelConstraints.LevelLength()) {
			/*if the probability of spawning turns out true and
			 * the current passPosition is past the max size a comet can be (to avoid collision), 
			 *then create a new comet
			 */
			int probability = Random.Range(1, 10);
			if((probability <= this.levelConstraints.CometProbability() && this.passPosition >= this.lastSpawnPosition + this.levelConstraints.MaxCometXLength() ) || (this.lastSpawnPosition - this.passPosition >= this.levelConstraints.MaxPlayerFlightDistance())) {

				//choose random spot within range of level height
				Vector3 spawnVector = new Vector3 (passPosition, Random.Range(this.levelConstraints.MinCometYSpawnHeight(),
				this.levelConstraints.MaxCometYSpawnHeight()), 0);
				
				//instantiate the comet
				Object instance = Instantiate(Resources.Load("Comet", typeof(GameObject)), spawnVector, transform.rotation);

				//Rigidbody2D cometInstance = Instantiate(comet, spawnVector, transform.rotation) as Rigidbody2D;

				//set the lastSpawnPosition to this current spawn
				this.lastSpawnPosition = this.passPosition;
			}


			//increment the passPosition 
			this.passPosition += 1;
		}

		//reset the passPosition to default for the next pass use
		this.passPosition = 0;
	}

	//the second pass for generating the level
	//this pass generates the meteorites that shoot down towards the player
	private void Pass2 () {

		//while we have not reached the end of the level
		while (this.passPosition < this.levelConstraints.LevelLength()) {
			/*if the probability of spawning turns out true and
			 * the current passPosition is past the max size a comet can be (to avoid collision), 
			 *then create a new comet
			 */
			int probability = Random.Range(1, 10);
			if(( probability <= this.levelConstraints.MeteoriteSpawnerProbability() && this.passPosition >= this.lastSpawnPosition + this.levelConstraints.MinMeteoriteSpawnDistance() ) || (this.lastSpawnPosition - this.passPosition >= this.levelConstraints.MaxMeteoriteSpawnDistance())) {
				
				//choose random spot within range of level height
				Vector3 spawnVector = new Vector3 (this.passPosition, 100, 0);
				
				//instantiate the comet
				Object instance = Instantiate(Resources.Load("meteoriteSpawn", typeof(GameObject)), spawnVector, transform.rotation);
				//Rigidbody2D meteoriteSpawnerInstance = Instantiate(meteoriteSpawn, spawnVector, transform.rotation) as Rigidbody2D;
				
				//set the lastSpawnPosition to this current spawn
				this.lastSpawnPosition = this.passPosition;
			}
			//increment the passPosition 
			this.passPosition += 1;
		}

		//reset the passPosition to default for the next pass use
		this.passPosition = 0;
	}

	//the final pass
	//this pass destroys this object because the generation is done
	private void FinalPass () {
		Destroy (gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class WorldMapInput : MonoBehaviour {
	
	//the list of levels
	private Transform[] levels = new Transform[5];
	
	//the transform of the cursor
	private Transform cursor;
	
	//the button iterator
	private int levelIterator = 0;
	
	//for horizontal axis control
	private float horizontal;
	
	//the time counter for input
	private float timeCounter = 0;
	
	//the time till next input allowed
	private float inputTime = 0.3f;


	// Use this for initialization
	void Start () {
		cursor = gameObject.transform;
		levels [0] = GameObject.Find ("Level1").transform;
		levels [1] = GameObject.Find ("Level2").transform;
		levels [2] = GameObject.Find ("Level3").transform;
		levels [3] = GameObject.Find ("Level4").transform;
		levels [4] = GameObject.Find ("Level5").transform;
		cursor.position = new Vector3(levels[levelIterator].transform.position.x + 0.2f, levels[levelIterator].transform.position.y, cursor.position.z);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		//Get values of vertical axis
		this.horizontal = Input.GetAxis ("Horizontal");

		//time the input for better control
		if(Time.time > this.timeCounter + this.inputTime)
		{
			//the cursor changes to the left level
			if(this.horizontal > 0)
			{
				if(levelIterator != 4)
				{
					levelIterator++;
					cursor.position = new Vector3(levels[levelIterator].transform.position.x + 0.2f, levels[levelIterator].transform.position.y, cursor.position.z);
				}
				//if cursor is at the right most level, loop to the first and left most level
				else
				{
					levelIterator = 0;
					cursor.position = new Vector3(levels[levelIterator].transform.position.x + 0.2f, levels[levelIterator].transform.position.y, cursor.position.z);
				}
				this.timeCounter = Time.time;
			}
			//else the cursor changes to the right level
			else if(this.horizontal < 0)
			{
				if(levelIterator != 0)
				{
					levelIterator--;
					cursor.position = new Vector3(levels[levelIterator].transform.position.x + 0.2f, levels[levelIterator].transform.position.y, cursor.position.z);
				}
				//if cursor is at the left most level, loop to the last and right most level
				else
				{
					levelIterator = 4;
					cursor.position = new Vector3(levels[levelIterator].transform.position.x + 0.2f, levels[levelIterator].transform.position.y, cursor.position.z);
				}
				this.timeCounter = Time.time;
			}
		}
		
		//check if action button is pressed, if so, choose whatever option the cursor is currently on
		if(Input.GetButtonDown("Submit"))
		{
			//choose cursor selection
			if(levelIterator == 0)
			{
				Level1();
			}
			else if(levelIterator == 1)
			{
				Level2();
			}
			else if(levelIterator == 2)
			{
				Level3();
			}
			else if(levelIterator == 3)
			{
				Level4();
			}
			else if(levelIterator == 4)
			{
				Level5();
			}
		}

		//check to see if the back button was pressed
		if(Input.GetButtonDown("Back")) {
			GoBackAScreen();
		}


	}


	//selects and loads level 1
	private void Level1 () {
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	//selects and loads level 2
	private void Level2 () {
		Application.LoadLevel (Application.loadedLevel + 2);
	}

	//selects and loads level 3
	private void Level3 () {
		Application.LoadLevel (Application.loadedLevel + 3);
	}

	//selects and loads level 4
	private void Level4 () {
		Application.LoadLevel (Application.loadedLevel + 4);
	}

	//selects and loads level 1
	private void Level5 () {
		Application.LoadLevel (Application.loadedLevel + 5);
	}

	//goes back to the title screen
	private void GoBackAScreen () {
		Application.LoadLevel (Application.loadedLevel - 1);
	}

}

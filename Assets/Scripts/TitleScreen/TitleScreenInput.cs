using UnityEngine;
using System.Collections;

public class TitleScreenInput : MonoBehaviour {

	//the list of buttons
	private Transform[] buttons = new Transform[3];

	//the transform of the cursor
	private Transform cursor;

	//the button iterator
	private int buttonIterator = 0;
	
	//for vertical axis control
	private float vertical;

	//the time counter for input
	private float timeCounter = 0;

	//the time till next input allowed
	private float inputTime = 0.3f;

	// Use this for initialization
	void Start () {
		cursor = gameObject.transform;
		buttons [0] = GameObject.Find ("NewGame").transform;
		buttons [1] = GameObject.Find ("LoadGame").transform;
		buttons [2] = GameObject.Find ("Quit").transform;
		cursor.position = new Vector3(buttons[buttonIterator].transform.position.x, buttons[buttonIterator].transform.position.y, cursor.position.z);
	}
	
	// Update is called once per frame
	void Update () {

	}

	//
	void FixedUpdate () {
		//Get values of vertical axis
		this.vertical = Input.GetAxis ("Vertical");

		if(Time.time > this.timeCounter + this.inputTime)
		{
			//the cursor changes buttons upwards
			if(this.vertical > 0)
			{
				if(buttonIterator != 0)
				{
					buttonIterator--;
					cursor.position = new Vector3(buttons[buttonIterator].transform.position.x, buttons[buttonIterator].transform.position.y, cursor.position.z);
				}
				//if at the top of the button list, loop to bottom of list
				else
				{
					buttonIterator = 2;
					cursor.position = new Vector3(buttons[buttonIterator].transform.position.x, buttons[buttonIterator].transform.position.y, cursor.position.z);
				}
				this.timeCounter = Time.time;
			}
			//else the cursor changes buttons downwards
			else if(this.vertical < 0)
			{
				if(buttonIterator != 2)
				{
					buttonIterator++;
					cursor.position = new Vector3(buttons[buttonIterator].transform.position.x, buttons[buttonIterator].transform.position.y, cursor.position.z);
				}
				//if at the bottom of the button list, loop to top of list
				else
				{
					buttonIterator = 0;
					cursor.position = new Vector3(buttons[buttonIterator].transform.position.x, buttons[buttonIterator].transform.position.y, cursor.position.z);
				}
				this.timeCounter = Time.time;
			}
		}

		//check if action button is pressed, if so, choose whatever option the cursor is currently on
		if(Input.GetButtonDown("Submit"))
		{
			//choose cursor selection
			if(buttonIterator == 0)
			{
				NewGame();
			}
			else if(buttonIterator == 1)
			{
				LoadGame();
			}
			else if(buttonIterator == 2)
			{
				ExitGame();
			}
		}
	}

	//start a new game
	private void NewGame () {
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	//load the game
	private void LoadGame () {

	}

	//exit the game
	private void ExitGame () {
		Application.Quit();
	}
}

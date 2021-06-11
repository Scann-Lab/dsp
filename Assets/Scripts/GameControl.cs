using UnityEngine;
using System.Collections;
using System;

public class GameControl : MonoBehaviour {

	public static GameControl control;

	//Input Variables
	//Participant identifier info
	public string participantNumber;

	//Is the experiment alternate or normal maze?
	public bool alternateExperiment;

	// Using gamepad or not?
	public bool UsingGamePad;

	// Number of laps to run
	public int LapsNumber;

	// These get set by default below
	public int currentLevelNo = 0;
	public string currentLevelName;
	public string currentTargetObject;
	public Vector3 currentLevelPosition;
	public bool startWriting = false;
	public bool movementLocked;
	public bool startedTrials = false;
	public string directory;
	public bool metadata = false;
	public string currentBox;
	public int laps;
	public int DSPtype;
	public bool Full;
	public string RepType;

	public ArrayList arrowList = new ArrayList{};

	public ArrayList levelPool = new ArrayList{"dsp1_02", "dsp1_03","dsp1_04","dsp1_05", "dsp1_06",
		"dsp1_07","dsp1_08", "dsp1_09","dsp1_10","dsp1_11","dsp1_13","dsp1_14", "dsp1_16",
		"dsp1_17","dsp1_18", "dsp1_19","dsp1_21", "dsp1_22","dsp1_23", "dsp1_24"};

	public ArrayList targetList = new ArrayList{"Picnic Table","Piano","Trash Can","Bookshelf","Harp","Trash Can",
		"Wheelbarrow", "Well","Piano","Wheelbarrow","Chair","Bookshelf",
		"Harp","Picnic Table", "Well", "Plant", "Mailbox","Stove","Plant","Telescope"};

	public ArrayList positionList = new ArrayList{new Vector3(174.2f,4f,171.2f),new Vector3(216.1f,4f,132.1f),
		new Vector3(216.1f,4f,132.1f),new Vector3(216.1f,4f,31.84f),new Vector3(216.1f,4f,31.84f),new Vector3(132.7f,4f,10.91f),new Vector3(132.7f,4f,10.91f),
		new Vector3(93.4f,4f,33.1f),new Vector3(93.4f,4f,33.1f),new Vector3(71.5f,4f,72.2f),new Vector3(32.2f,4f,31.9f),
		new Vector3(32.2f,4f,31.9f),new Vector3(13.2f,4f,153.35f),new Vector3(50.9f,4f,191.8f),new Vector3(50.9f,4f,191.8f),
		new Vector3(72.6f,4f,151.2f),new Vector3(112.6f,4f,113f),new Vector3(112.6f,4f,113f),new Vector3(131.5f,4f,152.1f),
		new Vector3(131.5f,4f,152.1f)};

	public ArrayList levelPoolAlt = new ArrayList{"dsp2_02", "dsp2_03","dsp2_04","dsp2_05", "dsp2_06",
		"dsp2_07","dsp2_08", "dsp2_09","dsp2_10","dsp2_11","dsp2_13","dsp2_14", "dsp2_16",
		"dsp2_17","dsp2_18", "dsp2_19","dsp2_21", "dsp2_22","dsp2_23", "dsp2_24"};

	public ArrayList targetListAlt = new ArrayList{"Clock",
		"Desk","Water cooler",
		"Streetlamp","Fridge",
		"Water cooler","Stepladder",
		"Bicycle","Desk",
		"Stepladder",
		"Statue","Streetlamp",
		"Fridge",
		"Clock", "Bicycle",
		"Swing",
		"Couch","TV",
		"Swing","Phonebooth"};

	public ArrayList positionListAlt = new ArrayList{new Vector3(171.3f,4f,171.0f),
		new Vector3(133.4f,4f,210.1f),new Vector3(133.4f,4f,210.1f),
		new Vector3(33.6f,4f,210.1f),new Vector3(33.6f,4f,210.1f),
		new Vector3(11.4f,4f,129.2f), new Vector3(11.4f,4f,129.2f),
		new Vector3(32.7f,4f,90.6f),new Vector3(32.7f,4f,90.6f),
		new Vector3(71.3f,4f,71.0f),
		new Vector3(33.18f,4f,31.30f),new Vector3(33.18f,4f,31.30f),
		new Vector3(151.9f,4f,12.30f),
		new Vector3(191.8f,4f,52.70f),new Vector3(191.8f,4f,52.70f),
		new Vector3(153.75f,4f,69.3f),
		new Vector3(112.6f,4f,111.5f),new Vector3(112.6f,4f,111.5f),
		new Vector3(153.75f,4f,131.9f),new Vector3(153.75f,4f,131.9f)};

	//A coroutine. Loads level after time delay.
	IEnumerator loadAfterDelay(string levelName, int seconds){
		yield return new WaitForSeconds(seconds);
		UnityEngine.SceneManagement.SceneManager.LoadScene (levelName);
	}

	public void goToLearning(){
		//loads alternate or nonalternate learning phase
		if (alternateExperiment == false) {
			StartCoroutine (loadAfterDelay ("Learning - Normal", 0));
		}
		else {
			StartCoroutine (loadAfterDelay ("Learning - Alternate", 0));
		}
	}

	//takes in a list of levels; teleports to a random level in list; removes that level from list.
	public void rollNextLevel(){
		//if the current level is 24 or under
		if (currentLevelNo < 20) {
			//for regular experiment
			if (alternateExperiment == false) {
				float randomGen = UnityEngine.Random.Range (0, levelPool.Count);
				int i = (int)randomGen;
				UnityEngine.SceneManagement.SceneManager.LoadScene ("Pre-Level Screen");
				string nextLevel = (string)levelPool [i];

				//sets the parameters for the next level before loading
				currentLevelName = nextLevel;
				currentTargetObject = (string)targetList [i];

				currentLevelPosition = (Vector3)positionList [i];
				StartCoroutine (loadAfterDelay ("Trials - Normal", 1));

				//increases current level indicator
				currentLevelNo = currentLevelNo + 1;
				Debug.Log ("Current Level Increased. Now: " + currentLevelNo);

				targetList.RemoveAt (i);
				levelPool.RemoveAt (i);
				positionList.RemoveAt (i);
			}

			//for alternate experiment
			else {
				float randomGen = UnityEngine.Random.Range (0, levelPoolAlt.Count);
				int i = (int)randomGen;
				UnityEngine.SceneManagement.SceneManager.LoadScene ("Pre-Level Screen");
				string nextLevel = (string)levelPoolAlt [i];

				//sets the parameters for the next level before loading
				currentLevelName = nextLevel;
				currentTargetObject = (string)targetListAlt [i];

				currentLevelPosition = (Vector3)positionListAlt [i];
				StartCoroutine (loadAfterDelay ("Trials - Alternate", 1));

				//increases current level indicator
				currentLevelNo = currentLevelNo + 1;
				Debug.Log ("Current Level Increased. Now: " + currentLevelNo);

				targetListAlt.RemoveAt (i);
				levelPoolAlt.RemoveAt (i);
				positionListAlt.RemoveAt (i);
			}
		}
		//after trials have concluded
		else {
			currentLevelNo = currentLevelNo + 1;
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Pre-Level Screen");
		}
	}

	//function for loading a single specified level.
	public void loadLevel(string levelName){
		if (alternateExperiment == false) {
			int i = levelPool.IndexOf (levelName);
			string nextLevel = (string)levelPool [i];
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Pre-Level Screen");

			//sets the parameters for the next level before loading
			currentLevelName = nextLevel;
			currentTargetObject = (string)targetList [i];
			currentLevelPosition = (Vector3)positionList [i];
			StartCoroutine (loadAfterDelay ("Trials - Normal", 1));
			currentLevelNo++;
			Debug.Log ("Current Level Increased. Now: " + currentLevelNo);

			//cleans up randomizer list
			targetList.RemoveAt (i);
			levelPool.RemoveAt (i);
			positionList.RemoveAt (i);
		} else{
			int i = levelPoolAlt.IndexOf (levelName);
			string nextLevel = (string)levelPoolAlt [i];
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Pre-Level Screen");

			//sets the parameters for the next level before loading
			currentLevelName = nextLevel;
			currentTargetObject = (string)targetListAlt [i];
			currentLevelPosition = (Vector3)positionListAlt [i];
			StartCoroutine (loadAfterDelay ("Trials - Alternate", 1));
			currentLevelNo++;
			Debug.Log ("Current Level Increased. Now: " + currentLevelNo);

			//cleans up randomizer list
			targetListAlt.RemoveAt (i);
			levelPoolAlt.RemoveAt (i);
			positionListAlt.RemoveAt (i);
		}
	}

	//Awake() occurs before the start of the game. This section creates the gamecontrol object and makes it indestructible.
	void Awake () {
		//if there is no gamecontrol object yet, make this one the control.
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		}

		//if a gamecontrol already exists, destroy this one.
		else if (control != this) {
			Destroy (gameObject);
		}
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class FirstPersonScript : MonoBehaviour {
	private Transform playerTransform;
	public CharacterController cc;
	int count = 0;
	float verticalRotation = 0;
	float upDownRange = 60.0f;
	double currTime = 0.0;
	string directory;

	float movementSpeed = 18.0f;
	float mouseSensitivity = 2.0f;

	//Toggles whether write to disk is enabled.
	public bool writeToDisk = true;


	// Use this for initialization
	void Start () {

		Debug.Log ("Loaded Player onto: " + SceneManager.GetActiveScene().name);
		Debug.Log ("Trial Number: " + GameControl.control.currentLevelNo);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		cc = GetComponent<CharacterController> ();

		if (GameControl.control.startedTrials == true){
			GameObject.Find ("Player").transform.position = GameControl.control.currentLevelPosition;
		}



		//writing data
		if(writeToDisk==true && GameControl.control.directory != null && GameControl.control.startedTrials == true)
		{
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(GameControl.control.directory, true))
			{
				if (GameControl.control.metadata == false) {
					//write in elements
					file.WriteLine ("ParticipantNo: " + GameControl.control.participantNumber);
					file.WriteLine ("ParticipantGen: " + GameControl.control.participantGender);
					file.WriteLine ("Stressor: " + GameControl.control.Stressor);
					file.WriteLine ("Exp Type?: " + GameControl.control.ExpType);
					file.WriteLine ("Repetition?: " + GameControl.control.RepType);
					file.WriteLine ("DSPType: " + GameControl.control.DSPtype);
					file.WriteLine ("AutoExperiment: " + GameControl.control.automaticExperiment);
					file.WriteLine ("Encoding Tours: " + GameControl.control.LapsNumber);
					GameControl.control.metadata = true;
				}
				if (GameControl.control.currentLevelNo > 0) {
					file.WriteLine ("!!" + GameControl.control.currentLevelNo + "_" + GameControl.control.currentLevelName);
					currTime = 0.0;
					count++;
					Debug.Log ("WROTE THE TRIAL " + "!!" + GameControl.control.currentLevelNo + "_" + GameControl.control.currentLevelName);
				}
			}
		} 
	}

	// Update is called once per frame
	void Update () {

		//Movement Mechanics
		//rotation
		float rotLeftRight = Input.GetAxis ("Mouse X") * mouseSensitivity;
		transform.Rotate (0, rotLeftRight, 0);
		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);

		//movement
		float forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis ("Horizontal") * movementSpeed;
		Vector3 speed = new Vector3 (sideSpeed, 0, forwardSpeed);
		speed = transform.rotation * speed; 

		if (cc.enabled) {
			cc.Move (speed * Time.deltaTime);
		}

		//disables the character controller if movementLocked == true
		if (GameControl.control.movementLocked == true) {
			cc.enabled = false;
			GameControl.control.startWriting = false;
		} 
		else {
			cc.enabled = true;
			GameControl.control.startWriting = true;

		}
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			movementSpeed = 30.0f;
		} 

		if (Input.GetKeyUp (KeyCode.LeftShift)){
			movementSpeed = 18.0f;
		}

		//writing data
		playerTransform = GameObject.Find ("Player").transform;
		Vector3 playerPos = playerTransform.position;
		Quaternion playerRot = playerTransform.rotation;

		if (writeToDisk == true && GameControl.control.directory != null && GameControl.control.startedTrials == true) {
			if (GameControl.control.startWriting == true && GameControl.control.currentLevelNo > 0) {
				using (System.IO.StreamWriter file = new System.IO.StreamWriter (GameControl.control.directory, true)) {
					currTime += Time.deltaTime;
					file.WriteLine (Math.Round (currTime, 2) + ":  " + playerPos.x + "," + playerPos.z + "," + playerRot.eulerAngles.y + "," + GameControl.control.currentBox);
					count++;
					//Debug.Log ("WROTE THE STEP: " + Math.Round (currTime, 2) + ":  " + playerPos.x + "," + playerPos.z);
				}
			}
		}

	}
}
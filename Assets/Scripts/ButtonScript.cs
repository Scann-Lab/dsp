using UnityEngine;
using System.Collections;
using System;

public class ButtonScript : MonoBehaviour {

	public UnityEngine.UI.InputField participant;
	public UnityEngine.UI.Toggle alternate;
	public UnityEngine.UI.InputField number;
	// public UnityEngine.UI.Toggle gamepad;

	public UnityEngine.UI.Text errorText;



	public void LoadLevel(){
		// gamepad = GameObject.Find("Gamepad Toggle");

		GameControl.control.participantNumber = participant.text;
		GameControl.control.alternateExperiment = alternate.isOn;
		// GameControl.control.UsingGamePad = gamepad.isOn;
		GameControl.control.LapsNumber = int.Parse(number.text);
		string name = GameControl.control.participantNumber;


		GameControl.control.directory = Application.dataPath +
		"\\..\\Participant_" + name +
		"_Date_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm") +
		".txt";
	}





	public void StartExperiment(){
		try{

			GameControl.control.participantNumber = participant.text;
			GameControl.control.alternateExperiment = alternate.isOn;
			// GameControl.control.UsingGamePad = gamepad.isOn;
			GameControl.control.LapsNumber = int.Parse(number.text);
			string name = GameControl.control.participantNumber;

			// Only valid entry at the moment - training + learning + trials = Full.
			GameControl.control.RepType = "Full";

			if (GameControl.control.alternateExperiment == false){
				GameControl.control.DSPtype = 1;
			}
			else{
				GameControl.control.DSPtype = 2;
			}


			GameControl.control.directory = Application.dataPath +
			"\\..\\Participant_" + name +
			"_Date_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm") +
			".txt";

			// Go right to the training level.
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Training Level");

		}
		catch (FormatException e){
			errorText.text = "Please enter all fields before starting\nCould be that the participant number is taken.";
		}

	}

}

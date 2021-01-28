using UnityEngine;
using System.Collections;
using System;

public class ButtonScript : MonoBehaviour {
	public UnityEngine.UI.InputField manualEntryField;

	public UnityEngine.UI.InputField participant;
	public UnityEngine.UI.InputField gender;
	//public UnityEngine.UI.InputField experiment;
	public UnityEngine.UI.InputField date;

	public UnityEngine.UI.InputField Stressor;
	//public UnityEngine.UI.InputField Rep;

	public UnityEngine.UI.Toggle alternate;
	public UnityEngine.UI.Toggle automatic;

	public UnityEngine.UI.Toggle bypass;
	public UnityEngine.UI.InputField number;

	public UnityEngine.UI.Toggle training;
	public UnityEngine.UI.Toggle Full;

	//boss specific toggles
	public UnityEngine.UI.Toggle RepPre;
	//public UnityEngine.UI.Toggle RepPost;	
	public UnityEngine.UI.Toggle Control;
	//public UnityEngine.UI.Toggle Tx;

	public UnityEngine.UI.Text errorText;

	public void DisplayText(){
		Debug.Log (manualEntryField.text);
	}

	public void LoadLevel(){
		try{
		GameControl.control.participantNumber = participant.text;
		GameControl.control.participantGender = int.Parse(gender.text);
		//GameControl.control.experimentType = experiment.text;
		GameControl.control.date = date.text;
		GameControl.control.alternateExperiment = alternate.isOn;
		GameControl.control.automaticExperiment = automatic.isOn;
		GameControl.control.bypassAll= bypass.isOn;
		GameControl.control.LapsNumber = int.Parse(number.text);
		//GameControl.control.Rep = Rep.text;
		GameControl.control.Stressor = int.Parse(Stressor.text);
		GameControl.control.RepPre = RepPre.isOn;
		//GameControl.control.RepPost = RepPost.isOn;
		GameControl.control.Control = Control.isOn;
		//GameControl.control.Tx = Tx.isOn;
		GameControl.control.Full = Full.isOn;
		GameControl.control.training = training.isOn;

		string name = GameControl.control.participantNumber;
		int genders = GameControl.control.participantGender;


		GameControl.control.directory = Application.dataPath + "\\..\\" + name + "_sex" + genders + 
				"_stressor" + GameControl.control.Stressor + "_ExpType"+ GameControl.control.ExpType + 
				"_Rep"+ GameControl.control.RepType + "_DSPtype" + GameControl.control.DSPtype + ".txt";

		if (!System.IO.File.Exists(GameControl.control.directory)){
			GameControl.control.loadLevel (manualEntryField.text);
		}
		else{
			throw new FormatException();
		}

		}
		catch (Exception e){
			errorText.text = "Please enter a valid level name";
			Debug.Log (GameControl.control.levelPoolAlt.IndexOf (manualEntryField.text));
			DisplayText();
		}
	}

	public void StartExperiment(){
		try{
			//sets the input variables in GameControl.cs
			GameControl.control.participantNumber = participant.text;
			GameControl.control.participantGender = int.Parse(gender.text);
			GameControl.control.date = date.text;
			GameControl.control.alternateExperiment = alternate.isOn;
			GameControl.control.automaticExperiment = automatic.isOn;
			GameControl.control.bypassAll= bypass.isOn;
			GameControl.control.LapsNumber = int.Parse(number.text);
			GameControl.control.Stressor = int.Parse(Stressor.text);
			GameControl.control.RepPre = RepPre.isOn;
			GameControl.control.Control = Control.isOn;
			GameControl.control.Full = Full.isOn;
			GameControl.control.training = training.isOn;


			//Logs the selected InputVariablesselected
			Debug.Log ("Participant: "+ GameControl.control.participantNumber);
			Debug.Log ("Gender: " + GameControl.control.participantGender);
			Debug.Log ("alternate: "+ GameControl.control.alternateExperiment);
			Debug.Log ("Automatic: "+ GameControl.control.automaticExperiment);

			string name = GameControl.control.participantNumber;
			int genders = GameControl.control.participantGender;

			if (GameControl.control.alternateExperiment == false){
				GameControl.control.DSPtype = 1;
				}
			else{
				GameControl.control.DSPtype = 2;
			}


			if (GameControl.control.RepPre == false && GameControl.control.Full == true){
				GameControl.control.RepType = "Full";
				}
			else {			
				if(GameControl.control.Full == false && GameControl.control.RepPre== true){
					GameControl.control.RepType = "Pre";
				}
			else {
				GameControl.control.RepType = "Post";
			}}



//			if (GameControl.control.RepPre == true){
//				GameControl.control.RepType = "Pre";
//			}
//			else{
//				GameControl.control.RepType = "Post";
//			}

			if (GameControl.control.Control == true){
				GameControl.control.ExpType = "Control";
			}
			else{
				GameControl.control.ExpType = "Treatment";
			}

			GameControl.control.directory = Application.dataPath + "\\..\\" + name + "_sex" + genders + 
				"_stressor" + GameControl.control.Stressor + "_ExpType"+ GameControl.control.ExpType + 
				"_Rep"+ GameControl.control.RepType + "_DSPtype" + GameControl.control.DSPtype + ".txt";


			if (!System.IO.File.Exists(GameControl.control.directory)){
				if (GameControl.control.bypassAll==true){
					GameControl.control.startedTrials = true;
					GameControl.control.rollNextLevel();
				}

//				else{
//					UnityEngine.SceneManagement.SceneManager.LoadScene ("Training Level");
//				}

				if (GameControl.control.training==true){
					UnityEngine.SceneManagement.SceneManager.LoadScene ("Training Level");
				}

				if (GameControl.control.Full == true){
					UnityEngine.SceneManagement.SceneManager.LoadScene ("Training Level");
				}

				if (GameControl.control.RepPre==true){
					GameControl.control.goToLearning();
				}

				if (GameControl.control.RepPre==false && GameControl.control.Full == false && GameControl.control.training==false){
						GameControl.control.rollNextLevel();
						GameControl.control.startedTrials = true;
					}


			}
			else{
				throw new FormatException();
			}
				
		}
		catch (FormatException e){
			errorText.text = "Please enter all fields before starting";
		}

	}

}

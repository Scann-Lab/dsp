using UnityEngine;
using System.Collections;

public class TrainingLevel : MonoBehaviour {

	public UnityEngine.UI.Text instructionText;

	void Start(){
		GameControl.control.currentLevelPosition = new Vector3 (40f, -19.83f, 43.13f);
		instructionText.enabled = false;

		if (GameControl.control.automaticExperiment==true) {
			StartCoroutine (ShowInstructions ());
		}
	}

	// Update is called once per frame
	void Update () {
		//If Ctrl+G is pressed any time during the training phase, goes to the learning phase
		if (Input.GetKey (KeyCode.F)) {
			GameControl.control.goToLearning ();
		}

		if (GameControl.control.training == true) {
			if (Input.GetKey (KeyCode.F)) {
				Application.Quit ();
			}
		}
	}

	IEnumerator ShowInstructions(){
		yield return new WaitForSeconds (15);
		instructionText.enabled = true;
	}
}


//
//if (GameControl.control.training == true) {
//	if (Input.GetKey (KeyCode.Escape)) {
//		Application.Quit ();
//	}
//}




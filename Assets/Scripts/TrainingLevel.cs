using UnityEngine;
using System.Collections;

public class TrainingLevel : MonoBehaviour {

	public UnityEngine.UI.Text instructionText;

	void Start(){
		GameControl.control.currentLevelPosition = new Vector3 (40f, -19.83f, 43.13f);
		instructionText.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		//If G is pressed any time during the training phase, goes to the learning phase
		if (Input.GetKey (KeyCode.G)) {
			GameControl.control.goToLearning ();
		}
	}

	IEnumerator ShowInstructions(){
		yield return new WaitForSeconds (15);
		instructionText.enabled = true;
	}
}

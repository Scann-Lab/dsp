using UnityEngine;
using System.Collections;

public class PreLevelScreen : MonoBehaviour {
	public UnityEngine.UI.Text textBox;
	public static PreLevelScreen control;


	// Use this for initialization
	void Start () {

		if (GameControl.control.currentLevelNo < 21) {
			textBox.text = "Loading next trial";

		}
		else {
			textBox.text = "FIN";		
		}

		if (GameControl.control.RepPre == true && GameControl.control.laps == GameControl.control.LapsNumber) {
			textBox.text = "FIN";
			Application.Quit ();
			}
		}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameControl.control.currentLevelNo >= 21) {
			if (Input.GetKey (KeyCode.Escape)) {
				Application.Quit ();
			}
		}

		if (GameControl.control.RepPre == true && GameControl.control.laps == GameControl.control.LapsNumber) {
			if (Input.GetKey (KeyCode.Escape)) {
				Application.Quit ();
			}
		}
	}
}
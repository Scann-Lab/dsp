using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePadSelect : MonoBehaviour
{

    public Toggle gamepad;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        gamepad = GetComponent<Toggle>();

    }

    // Update is called once per frame
    void Update()
    {
      if(gamepad.isOn){
        Debug.Log("i is 1 in load");
        i = 1;
      }else{
        Debug.Log("i is 0 in load");
        i = 0;
      }
      PlayerPrefs.SetInt("UsingGamePad", i);
    }
}

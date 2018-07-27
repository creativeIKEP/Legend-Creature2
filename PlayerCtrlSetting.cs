using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerCtrlSetting : MonoBehaviour {
    public KeyCode forward;
    public KeyCode back;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode dush;
    public KeyCode avoid;


	// Use this for initialization
	void Start () {
        dush = KeyCode.LeftShift;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorAlert : MonoBehaviour {
    float time = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);

        time += Time.deltaTime;
        if (time >= 10.0f) Destroy(gameObject);
	}
}

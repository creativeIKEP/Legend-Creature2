﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpParticleCtrl : MonoBehaviour {
    public GameObject warpParticle;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.layer==LayerMask.NameToLayer("Player")){
            //isActive = !isActive;
            warpParticle.SetActive(true);
            Debug.Log("Warp apper");
        }
	}
	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //isActive = !isActive;
            warpParticle.SetActive(false);
            Debug.Log("Warp disapper");
        }
	}
}

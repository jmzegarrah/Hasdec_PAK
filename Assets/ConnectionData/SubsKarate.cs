using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class SubsKarate : MonoBehaviour {

    public Image subTutor;
    //public float DelayTime = 10.0f;



    // Use this for initialization
    void Start () {
        if (LoginController.mostrarSubs == 1)
        {
            subTutor.enabled = true;
        }
        else {
            subTutor.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class SubsKarate : MonoBehaviour {

    public Image subTutor;
    public AudioSource soundTutor;
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
        if (LoginController.reproducirVoz == 1)
        {
            soundTutor.Play();
        }
        else
        {
            soundTutor.Stop();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}

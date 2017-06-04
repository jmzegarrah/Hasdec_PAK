using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CambiarMusica : MonoBehaviour {

    public Dropdown listaMusica;
    public AudioSource musicaReprodu;
    //public AudioMixerSnapshot volumenAbajo;
   // public AudioMixerSnapshot volemenArriba;
   // private float resetTime = .01f;
    public AudioClip Default;
    public AudioClip cancion1;
    public AudioClip cancion2;
    public AudioClip cancion3;
    public AudioClip cancion4;

    void Start()
    {
        musicaReprodu.Stop(); // just incase PlayOnAwake is ticked
    }

 
    void Update() {
        musicaReprodu = GetComponent<AudioSource>();
        switch (listaMusica.value) {
            case 0:
                //musicaReprodu.Stop();
                musicaReprodu.clip = cancion1;
 
                break;

            case 1:
                //musicaReprodu.Stop();
                musicaReprodu.clip = cancion2;
                //musicaReprodu.Play(); ;
                break;
            case 2:
                //musicaReprodu.Stop();
                musicaReprodu.clip = cancion3;
               // musicaReprodu.Play();
                break;
            case 3:
               // musicaReprodu.Stop();
                musicaReprodu.clip = cancion4;
                //musicaReprodu.Play();
                break;

            default:
               // musicaReprodu.Stop();
                musicaReprodu.clip = Default;
                
                break;
        }
        //FadeUp(resetTime);

        musicaReprodu.Play();

    }

}

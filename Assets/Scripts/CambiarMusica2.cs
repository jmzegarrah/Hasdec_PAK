using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CambiarMusica2 : MonoBehaviour {

   // public Dropdown listaMusica;
   public AudioSource musicaReprodu;
    //public AudioMixerSnapshot volumenAbajo;
    //public AudioMixerSnapshot volemenArriba;
    //private float resetTime = .01f;
   // public AudioClip Default;
    public AudioClip cancion1;
    public AudioClip cancion2;
    public AudioClip cancion3;
    public AudioClip cancion4;

    private void Awake()
    {
        musicaReprodu = GetComponent<AudioSource>();
    }

    void Start()
    {
        musicaReprodu.Stop(); // just incase PlayOnAwake is ticked
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            musicaReprodu.Stop();
            musicaReprodu.clip = cancion1;
            musicaReprodu.Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            musicaReprodu.Stop();
            musicaReprodu.clip = cancion2;
            musicaReprodu.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            musicaReprodu.Stop();
            musicaReprodu.clip = cancion3;
            musicaReprodu.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            musicaReprodu.Stop();
            musicaReprodu.clip = cancion4;
            musicaReprodu.Play();
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CambiarMusica : MonoBehaviour {

    public Dropdown listaMusica;
    public AudioSource musicaReprodu;
    public AudioMixerSnapshot volumenAbajo;
    public AudioMixerSnapshot volemenArriba;
    private float resetTime = .01f;
    public AudioClip Default;
    public AudioClip cancion1;
    public AudioClip cancion2;
    public AudioClip cancion3;
    public AudioClip cancion4;

    private void Awake()
    {
        musicaReprodu = GetComponent<AudioSource>();
    }

    public void PlayLevelMusic() {
        switch (listaMusica.value) {
            case 0:
                musicaReprodu.clip = Default;

                break;

            case 1:
                musicaReprodu.clip = cancion1;
                break;
            case 2:
                musicaReprodu.clip = cancion2;
                break;
            case 3:
                musicaReprodu.clip = cancion3;
                break;
            case 4:
                musicaReprodu.clip = cancion4;
                break;
            default:
                musicaReprodu.clip = Default;
                break;
        }
        FadeUp(resetTime);
        musicaReprodu.Play();
    }

    public void FadeUp(float fadeTime) {
        volumenAbajo.TransitionTo(fadeTime);
    }
    public void FadeDown(float fadeTime) {
        volumenAbajo.TransitionTo(fadeTime);
    }
}

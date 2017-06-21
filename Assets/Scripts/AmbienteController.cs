using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmbienteController : MonoBehaviour {
    public Image fondo;
    public Sprite Default;
    public Sprite ImgPer1;
    public Sprite ImgPer2;
    public Sprite ImgPer3;
    public Sprite ImgPer4;
    public Sprite ImgPer5;
    public Sprite ImgPer6;
    public Sprite ImgPer7;
    public Sprite ImgPer8;
    public Sprite ImgPer9;
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    public AudioSource audio4;
    public AudioSource audio5;

    // Use this for initialization
    void Start () {

        Debug.Log("ambiente" + LoginController.dojoSelected);
        Debug.Log("musica" + LoginController.musicSelected);
  

        switch (LoginController.dojoSelected+1) {
            case 1:
                fondo.sprite = Default;
                break;
            case 2:
                fondo.sprite = ImgPer1;
                break;
            case 3:
                fondo.sprite = ImgPer2;
                break;
            case 4:
                fondo.sprite = ImgPer3;
                break;
            case 5:
                fondo.sprite = ImgPer4;
                break;
            case 6:
                fondo.sprite = ImgPer5;
                break;
            case 7:
                fondo.sprite = ImgPer6;
                break;
            case 8:
                fondo.sprite = ImgPer7;
                break;
            case 9:
                fondo.sprite = ImgPer8;
                break;
            case 10:
                fondo.sprite = ImgPer9;
                break;

            default: break;
        }


        switch (LoginController.musicSelected+1) {
            case 1:
                audio1.Play();
                break;
            case 2:
                audio2.Play();
                break;
            case 3:
                audio3.Play();
                break;
            case 4:
                audio4.Play();
                break;
            case 5:
                audio5.Play();
                break;
            default: break;
        }
        AudioListener.volume = LoginController.volumenSelected;
    }

    public void stopMusic()
    {
        audio1.Stop();
        audio2.Stop();
        audio3.Stop();
        audio4.Stop();
        audio5.Stop();
    }

    // Update is called once per frame
    void Update () {
		
	}
}

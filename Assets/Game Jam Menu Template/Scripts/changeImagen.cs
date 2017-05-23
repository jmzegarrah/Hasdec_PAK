using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeImagen : MonoBehaviour {
    public Dropdown imagenSelector;
    public Image UsuarioImagen;
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


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (imagenSelector.value)
        {
            case 0:
                UsuarioImagen.sprite = Default;
                break;
            case 1:
                UsuarioImagen.sprite = ImgPer1;
                break;
            case 2:
                UsuarioImagen.sprite = ImgPer2;
                break;
            case 3:
                UsuarioImagen.sprite = ImgPer3;
                break;
            case 4:
                UsuarioImagen.sprite = ImgPer4;
                break;
            case 5:
                UsuarioImagen.sprite = ImgPer5;
                break;
            case 6:
                UsuarioImagen.sprite = ImgPer6;
                break;
            case 7:
                UsuarioImagen.sprite = ImgPer7;
                break;
            case 8:
                UsuarioImagen.sprite = ImgPer8;
                break;
            case 9:
                UsuarioImagen.sprite = ImgPer9;
                break;
            default:
                UsuarioImagen.sprite = Default;
                break;
        }

    }



}

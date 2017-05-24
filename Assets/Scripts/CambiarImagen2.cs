using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiarImagen2 : MonoBehaviour
{
    public Dropdown imagenSelector;
    public Image EscenarioImagen;
    public Sprite Default;
    public Sprite ImgConf1;
    public Sprite ImgConf2;
    public Sprite ImgConf3;
    public Sprite ImgConf4;
    public Sprite ImgConf5;
    public Sprite ImgConf6;
    public Sprite ImgConf7;
    public Sprite ImgConf8;
    public Sprite ImgConf9;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (imagenSelector.value)
        {
            case 0:
                EscenarioImagen.sprite = Default;
                break;
            case 1:
                EscenarioImagen.sprite = ImgConf1;
                break;
            case 2:
                EscenarioImagen.sprite = ImgConf2;
                break;
            case 3:
                EscenarioImagen.sprite = ImgConf3;
                break;
            case 4:
                EscenarioImagen.sprite = ImgConf4;
                break;
            case 5:
                EscenarioImagen.sprite = ImgConf5;
                break;
            case 6:
                EscenarioImagen.sprite = ImgConf6;
                break;
            case 7:
                EscenarioImagen.sprite = ImgConf7;
                break;
            case 8:
                EscenarioImagen.sprite = ImgConf8;
                break;
            case 9:
                EscenarioImagen.sprite = ImgConf9;
                break;
            default:
                EscenarioImagen.sprite = Default;
                break;
        }

    }



}
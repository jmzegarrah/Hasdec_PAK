﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class PerfilController : MonoBehaviour
{

    public InputField UsuSobNom;
    public InputField UsuNomApe;
    public InputField UsuSex;
    public Dropdown UsuIma;
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
    public GameObject ventanaConfirmacionEliminar;
    public GameObject ventanaPregunta;
    public InputField UsuResPreSec;
    public Dropdown UsuPreSec;


    void Start()
    {
        string url = "http://localhost/PAK_VerPerfil/PAK_DatosUsuario.php?" + "UsuSobNom=" +"gonzqh";
   
        StartCoroutine(loadData(url));
      
    }


    public void cambiarPregunta() {
        
        string url = "http://localhost/PAK_VerPerfil/PAK_CambiarPregunta.php?" + "UsuSobNom=" + "gonzqh"+"&UsuResPreSec="+ UsuResPreSec.text
            +"&UsuPreSec="+ (UsuPreSec.value+1);
        StartCoroutine(changeQuestion(url));
    }

    IEnumerator changeQuestion(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        ventanaPregunta.SetActive(false);       

    }

    public void eliminarCuenta() {
        string url = "http://localhost/PAK_VerPerfil/PAK_EliminarUsuario.php?" + "UsuSobNom=" + "gonzqh";
        StartCoroutine(deleteUser(url));
    }
    IEnumerator deleteUser(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        ventanaConfirmacionEliminar.SetActive(false);
        SceneManager.LoadScene("Log-Reg");

    }

        IEnumerator loadData(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
            string nativedata = conecction.text;
            string[] data = nativedata.Split(',');
            
            UsuNomApe.text = data[0];
            UsuSobNom.text = data[1];
        if (data[2].Equals("1"))
        {
            UsuSex.text = "Masculino";
        }
        else {
            UsuSex.text = "Femenino";
        }
        UsuIma.value = Int32.Parse(data[3])-1;
        
        String img = data[4].Substring(0,7);
        Debug.Log("," + img + ",");
        switch (img)
        {
            case "Default":
                UsuarioImagen.sprite = Default;
                break;
            case "ImgPer1":
                UsuarioImagen.sprite = ImgPer1;
                break;
            case "ImgPer2":
                UsuarioImagen.sprite = ImgPer2;
                break;
            case "ImgPer3":
                UsuarioImagen.sprite = ImgPer3;
                break;
            case "ImgPer4":
                UsuarioImagen.sprite = ImgPer4;
                break;
            case "ImgPer5":
                UsuarioImagen.sprite = ImgPer5;
                break;
            case "ImgPer6":
                UsuarioImagen.sprite = ImgPer6;
                break;
            case "ImgPer7":
                UsuarioImagen.sprite = ImgPer7;
                break;
            case "ImgPer8":
                UsuarioImagen.sprite = ImgPer8;
                break;
            case "ImgPer9":
                UsuarioImagen.sprite = ImgPer9;
                break;
            default:
                UsuarioImagen.sprite = Default;
                break;
        }

       

        Debug.Log("Datos Cargados");
    //     }
    }


}


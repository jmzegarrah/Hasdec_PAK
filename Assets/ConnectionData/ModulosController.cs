using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;


public class ModulosController : MonoBehaviour {
    public Image lockNaturales;
    public Image lockCuerpo;
    public Image lockAtaque;
    public Image lockDefensa;
    //public string usuarioTemp = "vilmapalma8";
    private string usuarioTemp = LoginController.usuario;
    public static String modulo = "";

    private void Start()
    {
        extraerLockNaturales();
        extraerLockCuerpo();
       extraerLockAtaque();
        extraerLockDefensa();

     }

    public void extraerLockNaturales() {
        string url = "http://localhost/PAK_Modulos/PAK_getLockMod.php?" + "UsuSobNom=" + usuarioTemp + "&NumMod=1";
        StartCoroutine(Lock(url, lockNaturales));
    }
    public void extraerLockCuerpo()
    {
        string url = "http://localhost/PAK_Modulos/PAK_getLockMod.php?" + "UsuSobNom=" + usuarioTemp + "&NumMod=2";
        StartCoroutine(Lock(url, lockCuerpo));
    }
    public void extraerLockAtaque()
    {
        string url = "http://localhost/PAK_Modulos/PAK_getLockMod.php?" + "UsuSobNom=" + usuarioTemp + "&NumMod=3";
        StartCoroutine(Lock(url, lockAtaque));
    }
    public void extraerLockDefensa()
    {
        string url = "http://localhost/PAK_Modulos/PAK_getLockMod.php?" + "UsuSobNom=" + usuarioTemp + "&NumMod=4";
        StartCoroutine(Lock(url, lockDefensa));
    }

    IEnumerator Lock(string url, Image modulo)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("Query"))
        {
            Debug.Log("No se encuentran datos");
        }
        else
        {
            if (conecction.text.Contains("1")) {
                modulo.enabled = false;
            }
        }
    }


    public void regresarPrincipal() {
        SceneManager.LoadScene("Scene");
        Debug.Log("Se encuentra en pantalla kinect");
    }

    public void movPosNaturales() {
        if(!lockNaturales.enabled)
        SceneManager.LoadScene("Pantalla Carga");
        modulo = "PosNaturales";
    }
    public void movCuerpo()
    {
        if (!lockCuerpo.enabled)
            SceneManager.LoadScene("Pantalla Carga");
        modulo = "Cuerpo";
    }
    public void movAtaque()
    {
        if (!lockAtaque.enabled)
            SceneManager.LoadScene("Pantalla Carga");
        modulo = "Ataque";
    }
    public void movDefensa()
    {
        if (!lockDefensa.enabled)
            SceneManager.LoadScene("Pantalla Carga");
        modulo = "Defensa";
    }

}

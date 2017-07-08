using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.Linq;


public class LoginController : MonoBehaviour
{
    public InputField UsuSobNom;
    public InputField UsuCon;
    public GameObject logerror;
    public GameObject logsuccess;
    public static string usuario = "";
    public static float volumenSelected = 1f;


//    public static int musicSelected = 0;
  //  public static int dojoSelected = 0;
  //  public static int reproducirVoz = 0;
   // public static int mostrarSubs = 0;
   // public static int mostrarmePantalla = 0;
   // public static int mostrarTips = 0;


    public static int musicSelected ;
    public static int dojoSelected ;
    public static int reproducirVoz;
    public static int mostrarSubs ;
    public static int mostrarmePantalla ;
    public static int mostrarTips ;
    public static int userSex;



    void Start()
    {
      
        logerror.SetActive(false);
        logsuccess.SetActive(false);
    }


    public void ValidarUsuario()
    {

        if (UsuSobNom.text.Length > 0 && UsuCon.text.Length >= 8)
        {
                 string url = "http://localhost/PAK_Login.php?"
                           + "UsuSobNom=" + UsuSobNom.text + "&" +
                           "UsuCon=" + UsuCon.text;

                usuario = UsuSobNom.text;


                StartCoroutine(LoginUser(url));

        }
        else
        {
            logerror.SetActive(true);
            
        }
    }



    IEnumerator LoginUser(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("null"))
        {
            logsuccess.SetActive(false);
            logerror.SetActive(true);
        }
        else
        {

            try
            {
                logerror.SetActive(false);
                logsuccess.SetActive(true);
                string[] configuracion = conecction.text.Split('#');

                musicSelected = Int32.Parse(configuracion[0]) - 1;
                dojoSelected = Int32.Parse(configuracion[1]) - 1;
                reproducirVoz = Int32.Parse(configuracion[2]);
                mostrarSubs = Int32.Parse(configuracion[3]);
                mostrarmePantalla = Int32.Parse(configuracion[4]);
                mostrarTips = Int32.Parse(configuracion[5]);
                userSex = Int32.Parse(configuracion[6]);

                SceneManager.LoadScene("Scene");
                Debug.Log("Te encuentras en menu principal");

            }
            catch (Exception) {
                logerror.SetActive(true);
                logsuccess.SetActive(false);
            }
           
        }
    }
}



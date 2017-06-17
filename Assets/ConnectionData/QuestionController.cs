using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestionController : MonoBehaviour {
    public InputField UsuSobNom;
    public InputField UsuResPreSec;
    public InputField UsuCon;
    public InputField UsuConfirmacion;
    public GameObject valerror;
    public GameObject ventanaPassword;
    public GameObject ventanaQuestion;
    string usuario = "";
    public Text UsuPreSec;


    // Use this for initialization
    void Start () {
        valerror.SetActive(false);
        
    }

    public void validarUsuario() {
        if (UsuSobNom.text.Length > 0)
        {
            string url = "http://localhost/PAK_PreguntaSeguridad/PAK_ValidarUsuario.php?"
                      + "UsuSobNom=" + UsuSobNom.text;
            Debug.Log(url);
            usuario = UsuSobNom.text;
            StartCoroutine(ValidateUser(url));

        }
        else
        {
            valerror.SetActive(true);

        }
    }
    public void validarRespuesta()
    {
        if (UsuResPreSec.text.Length > 0 && UsuResPreSec.text.Length < 20)
        {
            string url = "http://localhost/PAK_PreguntaSeguridad/PAK_ValidarRespuesta.php?"
                      + "UsuSobNom=" + UsuSobNom.text + "&UsuResPreSec=" + UsuResPreSec.text;
            Debug.Log(url);
            StartCoroutine(ValidateQuestion(url));

        }
        else
        {
          //  valQuestionerror.SetActive(true);
        }
    }



    public void cambiarContrasena()
    {
        if (UsuCon.text.Length >= 8 && UsuCon.text.Length < 20)
        {
            if (UsuCon.text.Equals(UsuConfirmacion.text))
            {
                string url = "http://localhost/PAK_PreguntaSeguridad/PAK_CambiarContrasena.php?"
                          + "UsuCon=" + UsuCon.text + "&UsuSobNom=" + usuario;
                Debug.Log(url);
                StartCoroutine(ChangePassword(url));
            }
            else {
                Debug.Log("Las contrasenas no coinciden");
            }

        }
        else
        {
            valerror.SetActive(true);

        }
    }



    IEnumerator ValidateQuestion(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("0"))
        {
            //valQuestionerror.SetActive(true);
        }
        else
        {
            UsuPreSec.text = conecction.text;
            Debug.Log("Correcto");
            //valQuestionerror.SetActive(false);
            Debug.Log(conecction.text);
            ventanaQuestion.SetActive(false);
            ventanaPassword.SetActive(true);
        }
    }

    IEnumerator ValidateUser(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("0"))
        {
            valerror.SetActive(true);            
        }
        else
        {
            
            UsuPreSec.text = conecction.text;
            valerror.SetActive(false);
            Debug.Log(conecction.text);
        }
    }


    IEnumerator ChangePassword(string url)
    {
        Debug.Log(url);
        Debug.Log("el usuario es: " + usuario);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("Query"))
        {
           
           
        }
        else
        {
            UsuPreSec.text = conecction.text;
            ventanaPassword.SetActive(false);
            Debug.Log(conecction.text);
        }
    }




    
}

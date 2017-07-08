using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class LogrosController : MonoBehaviour {

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
    public Text UsuNomApe;
    public Text movRealizados;
    public Text mov1Y;
    public Text mov1N;
    public Text mov2Y;
    public Text mov2N;
    public Text mov3Y;
    public Text mov3N;
    public Text mov4Y;
    public Text mov4N;
    public Text mov5Y;
    public Text mov5N;
    public Text mov6Y;
    public Text mov6N;
    public Text mov7Y;
    public Text mov7N;
    public Text mov8Y;
    public Text mov8N;

    void Start () {
        string url = "http://localhost/PAK_Logros/PAK_LogrosUsuario.php?" + "UsuSobNom=" + LoginController.usuario;
        string url2 = "http://localhost/PAK_Logros/PAK_Historial.php?" + "UsuSobNom=" + LoginController.usuario;

        StartCoroutine(loadData(url));
        StartCoroutine(loadHistory(url2));
    }
    IEnumerator loadData(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        string nativedata = conecction.text;
        string[] data = nativedata.Split('#');

        UsuNomApe.text = data[0];
        String ImgNom = data[1].Substring(0, 7);
        
        switch (ImgNom)
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

    }

    IEnumerator loadHistory(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        string nativedata = conecction.text;
        if (conecction.text.Contains("null"))
        {

        }
        else {
            int historial = Int32.Parse(conecction.text);
            Debug.Log("Desbloqueado " + historial);
            movRealizados.text = "Movmientos Completados :  " + historial + "/10";

            switch (historial) {

                case 0:
                    mov1Y.enabled=false;
                    mov1N.enabled=true;
                    mov2Y.enabled = false;
                    mov2N.enabled = true;
                    mov3Y.enabled = false;
                    mov3N.enabled = true;
                    mov4Y.enabled = false;
                    mov4N.enabled = true;
                    mov5Y.enabled = false;
                    mov5N.enabled = true;
                    mov6Y.enabled = false;
                    mov6N.enabled = true;
                    mov7Y.enabled = false;
                    mov7N.enabled = true;
                    mov8Y.enabled = false;
                    mov8N.enabled = true;
                    break;

                case 1:
                    mov1Y.enabled = true;
                    mov1N.enabled = false;
                    mov2Y.enabled = false;
                    mov2N.enabled = true;
                    mov3Y.enabled = false;
                    mov3N.enabled = true;
                    mov4Y.enabled = false;
                    mov4N.enabled = true;
                    mov5Y.enabled = false;
                    mov5N.enabled = true;
                    mov6Y.enabled = false;
                    mov6N.enabled = true;
                    mov7Y.enabled = false;
                    mov7N.enabled = true;
                    mov8Y.enabled = false;
                    mov8N.enabled = true;
                    break;
                case 2:
                    mov1Y.enabled = true;
                    mov1N.enabled = false;
                    mov2Y.enabled = true;
                    mov2N.enabled = false;
                    mov3Y.enabled = false;
                    mov3N.enabled = true;
                    mov4Y.enabled = false;
                    mov4N.enabled = true;
                    mov5Y.enabled = false;
                    mov5N.enabled = true;
                    mov6Y.enabled = false;
                    mov6N.enabled = true;
                    mov7Y.enabled = false;
                    mov7N.enabled = true;
                    mov8Y.enabled = false;
                    mov8N.enabled = true;
                    break;
                case 3:
                    mov1Y.enabled = true;
                    mov1N.enabled = false;
                    mov2Y.enabled = true;
                    mov2N.enabled = false;
                    mov3Y.enabled = true;
                    mov3N.enabled = false;
                    mov4Y.enabled = false;
                    mov4N.enabled = true;
                    mov5Y.enabled = false;
                    mov5N.enabled = true;
                    mov6Y.enabled = false;
                    mov6N.enabled = true;
                    mov7Y.enabled = false;
                    mov7N.enabled = true;
                    mov8Y.enabled = false;
                    mov8N.enabled = true;
                    break;
                case 4:
                    mov1Y.enabled = true;
                    mov1N.enabled = false;
                    mov2Y.enabled = true;
                    mov2N.enabled = false;
                    mov3Y.enabled = true;
                    mov3N.enabled = false;
                    mov4Y.enabled = true;
                    mov4N.enabled = false;
                    mov5Y.enabled = false;
                    mov5N.enabled = true;
                    mov6Y.enabled = false;
                    mov6N.enabled = true;
                    mov7Y.enabled = false;
                    mov7N.enabled = true;
                    mov8Y.enabled = false;
                    mov8N.enabled = true;
                    break;
                case 5:
                    mov1Y.enabled = true;
                    mov1N.enabled = false;
                    mov2Y.enabled = true;
                    mov2N.enabled = false;
                    mov3Y.enabled = true;
                    mov3N.enabled = false;
                    mov4Y.enabled = true;
                    mov4N.enabled = false;
                    mov5Y.enabled = true;
                    mov5N.enabled = false;
                    mov6Y.enabled = false;
                    mov6N.enabled = true;
                    mov7Y.enabled = false;
                    mov7N.enabled = true;
                    mov8Y.enabled = false;
                    mov8N.enabled = true;
                    break;
                case 6:
                    mov1Y.enabled = true;
                    mov1N.enabled = false;
                    mov2Y.enabled = true;
                    mov2N.enabled = false;
                    mov3Y.enabled = true;
                    mov3N.enabled = false;
                    mov4Y.enabled = true;
                    mov4N.enabled = false;
                    mov5Y.enabled = true;
                    mov5N.enabled = false;
                    mov6Y.enabled = true;
                    mov6N.enabled = false;
                    mov7Y.enabled = false;
                    mov7N.enabled = true;
                    mov8Y.enabled = false;
                    mov8N.enabled = true;
                    break;
                case 7:
                    mov1Y.enabled = true;
                    mov1N.enabled = false;
                    mov2Y.enabled = true;
                    mov2N.enabled = false;
                    mov3Y.enabled = true;
                    mov3N.enabled = false;
                    mov4Y.enabled = true;
                    mov4N.enabled = false;
                    mov5Y.enabled = true;
                    mov5N.enabled = false;
                    mov6Y.enabled = true;
                    mov6N.enabled = false;
                    mov7Y.enabled = true;
                    mov7N.enabled = false;
                    mov8Y.enabled = false;
                    mov8N.enabled = true;
                    break;
                case 8:
                    mov1Y.enabled = true;
                    mov1N.enabled = false;
                    mov2Y.enabled = true;
                    mov2N.enabled = false;
                    mov3Y.enabled = true;
                    mov3N.enabled = false;
                    mov4Y.enabled = true;
                    mov4N.enabled = false;
                    mov5Y.enabled = true;
                    mov5N.enabled = false;
                    mov6Y.enabled = true;
                    mov6N.enabled = false;
                    mov7Y.enabled = true;
                    mov7N.enabled = false;
                    mov8Y.enabled = true;
                    mov8N.enabled = false;
                    break;
            }
        }
        
    }

    public void regresar() {
        SceneManager.LoadScene("Scene");
    }


}

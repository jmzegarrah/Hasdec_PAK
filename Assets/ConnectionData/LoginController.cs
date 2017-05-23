using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
    public InputField UsuSobNom;
    public InputField UsuCon;
    public GameObject logerror;
    public GameObject logsuccess;


    void Start()
    {
      
        logerror.SetActive(false);
    }

    public void ValidarUsuario()
    {

        if (UsuSobNom.text.Length > 0 && UsuSobNom.text.Length < 10 && UsuCon.text.Length > 0 && UsuCon.text.Length < 10)
        {
                 string url = "http://localhost/PAK_Login.php?"
                           + "UsuSobNom=" + UsuSobNom.text + "&" +
                           "UsuCon=" + UsuCon.text;

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
        if (conecction.text==("0"))
        {
            logsuccess.SetActive(true);
            logerror.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Scene");
            Debug.Log("Te encuentras en logros");
            logerror.SetActive(false);
        }
    }
}



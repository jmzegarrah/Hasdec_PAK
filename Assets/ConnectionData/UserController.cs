using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    public InputField usuNomApe;
    public Dropdown UsuSex;
    public InputField UsuSobNom;
    public InputField UsuCon;
    public InputField UsuConConfirmacion;
    public Dropdown UsuPreSec;
    public InputField UsuResPreSec;
    public GameObject error;
    public GameObject success;
    string UsuIma = "1";
    string UsuEstPriSes = "1";
    string UsuEst = "1";
    string HisCod = "1";
    string PerCod = "1";

    void Start()
    {
        success.SetActive(false);
        error.SetActive(false);
    }

    public void RegistrarUsuario()
    {

        if (UsuSobNom.text.Length > 0 && UsuSobNom.text.Length < 10 && UsuCon.text.Length > 0 && UsuCon.text.Length < 10
           && UsuConConfirmacion.text.Length > 0 && UsuConConfirmacion.text.Length < 10)
        {
            if (UsuCon.text.Equals(UsuConConfirmacion.text))
            {

                string url = "http://localhost/PAK_AgregarUsuario.php?"
                           + "UsuNomApe=" +usuNomApe.text + "&" +
                           "UsuIma=" + UsuIma + "&" +
                           "UsuSex=" + UsuSex.value.ToString() + "&" +
                           "UsuSobNom=" + UsuSobNom.text + "&" +
                           "UsuCon=" + UsuCon.text + "&" +
                           "UsuPreSec=" + UsuPreSec.value.ToString() + "&" +
                           "UsuResPreSec=" + UsuResPreSec.text + "&" +
                           "UsuEstPriSes=" + UsuEstPriSes + "&" +
                           "UsuEst=" + UsuEst + "&" +
                           "HisCod=" + HisCod + "&" +
                           "PerCod=" + PerCod;

                StartCoroutine(AddUser(url));
                


            }
            else
            {
                error.SetActive(true);
                success.SetActive(false);
            }
        }
        else
        {
            error.SetActive(true);
            success.SetActive(false);
        }
    }



    IEnumerator AddUser(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (!conecction.text.Contains("Query"))
        {
            success.SetActive(true);
            error.SetActive(false);
        }
        else {
            Debug.Log("El usuario ya existe");
            success.SetActive(false);
            error.SetActive(true);
        }
    }
}



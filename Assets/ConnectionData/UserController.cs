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
    string UsuMus = "1";
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

        if (UsuSobNom.text.Length >0 && UsuSobNom.text.Length < 20 && UsuCon.text.Length >= 8 && UsuCon.text.Length < 20
           && UsuConConfirmacion.text.Length >= 8 && UsuConConfirmacion.text.Length < 20)
        {
            if (UsuCon.text.Equals(UsuConConfirmacion.text))
            {
                string pregunta = (UsuPreSec.value + 1).ToString();
                string nombreApellido = usuNomApe.text;
                Debug.Log(nombreApellido);
                nombreApellido = nombreApellido.Replace(" ","+");    
                Debug.Log(nombreApellido);
 
                string url = "http://localhost/PAK_AgregarUsuario.php?"
                           + "UsuNomApe=" +nombreApellido + "&" +
                           "UsuIma=" + UsuIma + "&" +
                           "UsuSex=" + UsuSex.value.ToString() + "&" +
                           "UsuSobNom=" + UsuSobNom.text + "&" +
                           "UsuCon=" + UsuCon.text + "&" +
                           "UsuPreSec=" + pregunta + "&" +
                           "UsuResPreSec=" + UsuResPreSec.text + "&" +
                           "UsuEstPriSes=" + UsuEstPriSes + "&" +
                           "UsuMus=" + UsuMus + "&" +
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
        Debug.Log(conecction.text);
        if (conecction.text.Contains("Query"))
        {
            Debug.Log("El usuario ya existe");
            success.SetActive(false);
            error.SetActive(true);
        }
        else {
            
            success.SetActive(true);
            error.SetActive(false);
        }
    }
}



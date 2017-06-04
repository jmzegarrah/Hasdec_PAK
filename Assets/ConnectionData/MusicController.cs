using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {
    public GameObject cuadroConfirmacionGuardar;
    public GameObject errorGuardar;
    public InputField UsuSobNom;
    public InputField UsuNomApe;
    public Dropdown UsuIma;

    void Start () {
        cuadroConfirmacionGuardar.SetActive(false);

    }
    public void AbrirGuardar() {
        cuadroConfirmacionGuardar.SetActive(true);
        errorGuardar.SetActive(false);
    }
    public void ConfirmarGuardar() {

        if (UsuNomApe.text.Length > 0 && UsuNomApe.text.Length <= 20 && UsuSobNom.text.Length > 0 && UsuSobNom.text.Length <= 20)
        {

            string url = "http://localhost/PAK_VerPerfil/PAK_ActualizarUsuario.php?" 
           + "UsuNomApe=" + UsuNomApe.text
           + "&UsuIma=" + (UsuIma.value + 1)
           + "&UsuSobNom=" + UsuSobNom.text
           + "&UsuSobAct=" + LoginController.usuario;
            StartCoroutine(saveChanges(url));
        }
        else {
            errorGuardar.SetActive(true);
        }       
    }

    IEnumerator saveChanges(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("Query"))
        {
            errorGuardar.SetActive(true);
        }
        else
        {
            LoginController.usuario = UsuSobNom.text;
            cuadroConfirmacionGuardar.SetActive(false);
            Debug.Log(conecction.text);
        }
    }


    public void CancelarGuardar()
    {
        cuadroConfirmacionGuardar.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}

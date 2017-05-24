using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SesionController : MonoBehaviour {

    // Use this for initialization
    public void cerrarSesion() {
        LoginController.usuario = "";
        SceneManager.LoadScene("Log-Reg");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PosNatController : MonoBehaviour {

    public void regresar() {
        SceneManager.LoadScene("ModulosEntrenamiento");
    }
    public void abrirJoe()
    {
        SceneManager.LoadScene("Joe");
    }
    public void abrirKamae()
    {
        SceneManager.LoadScene("Kamae");
    }
}

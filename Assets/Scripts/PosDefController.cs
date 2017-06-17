using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PosDefController : MonoBehaviour {

    public void regresar()
    {
        SceneManager.LoadScene("ModulosEntrenamiento");
    }
    public void abrirUke()
    {
        SceneManager.LoadScene("Uke");
    }
    public void abrirAge()
    {
        SceneManager.LoadScene("Age");
    }
}

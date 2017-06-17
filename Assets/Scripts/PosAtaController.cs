using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PosAtaController : MonoBehaviour {


    public void regresar()
    {
        SceneManager.LoadScene("ModulosEntrenamiento");
    }
    public void abrirTsuki()
    {
        SceneManager.LoadScene("Tsuki");
    }
    public void abrirZuki()
    {
        SceneManager.LoadScene("Oi Zuki");
    }
}

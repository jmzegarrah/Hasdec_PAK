using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PosCueController : MonoBehaviour {

    public void regresar()
    {
        SceneManager.LoadScene("ModulosEntrenamiento");
    }
    public void abrirKokutsu()
    {
        SceneManager.LoadScene("Kokutsu");
    }
    public void abrirZenkutsu()
    {
        SceneManager.LoadScene("Zenkutsu");
    }
}

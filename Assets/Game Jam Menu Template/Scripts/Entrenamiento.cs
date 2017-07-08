using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrenamiento : MonoBehaviour {


    // Use this for initialization
	public void abrirEntrenamiento() {
        SceneManager.LoadScene("CargandoKinect");
        Debug.Log("Se encuentra en pantalla kinect");
    }
    public void abrirLogros()
    {
        SceneManager.LoadScene("LogrosPAK");
        Debug.Log("Se encuentra en pantalla logros");
    }

}

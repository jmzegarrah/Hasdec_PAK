using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abrirPreguntaSeguridad : MonoBehaviour {


    public GameObject PreguntadeSeguridad;
    // Use this for initialization

    public void abrirPregunta()
    {
        Debug.Log("Hizo click");
        PreguntadeSeguridad.SetActive(true);
    }

    public void cancelar()
    {
                  PreguntadeSeguridad.SetActive(false);
      
    }
    public void validar()
    {
        Debug.Log("cerro pregunta apra abrir contrasena");
        PreguntadeSeguridad.SetActive(false);

    }

    void Start () {
        PreguntadeSeguridad.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}



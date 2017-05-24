using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abrirContrasena : MonoBehaviour
{
    public GameObject Contrasena;
    public GameObject valerror;
    // Use this for initialization

    public void abrirContrasenaWindow()
    {
        Debug.Log("abrio ventana de contrasena");
        Contrasena.SetActive(true);
        valerror.SetActive(false);
    }

    public void cancelar()
    {
        Contrasena.SetActive(false);

    }

    public void Guardar()
    {
        Contrasena.SetActive(false);

    }

    void Start()
    {
        Contrasena.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}



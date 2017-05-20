using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abrirConfirmacion : MonoBehaviour
{


    public GameObject CuadroConfirmacion;
    // Use this for initialization



    public void abrir()
    {
        CuadroConfirmacion.SetActive(true);
    }
    public void aceptar()
    {
        CuadroConfirmacion.SetActive(false);
    }
    
    public void cancelar()
    {
        CuadroConfirmacion.SetActive(false);

    }

     void Start()
    {
        CuadroConfirmacion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}



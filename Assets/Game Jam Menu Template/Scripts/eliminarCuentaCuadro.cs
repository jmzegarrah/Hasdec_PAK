using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eliminarCuentaCuadro : MonoBehaviour
{


    public GameObject CuadroEliminar;
    // Use this for initialization

    public void abrir()
    {
        CuadroEliminar.SetActive(true);
    }
    public void eliminarCuenta()
    {
        CuadroEliminar.SetActive(false);
    }

    public void cancelar()
    {
        CuadroEliminar.SetActive(false);

    }

    void Start()
    {
        CuadroEliminar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abrirGuardarCambios : MonoBehaviour {
    public GameObject CuadroGuardar;
    // Use this for initialization
    void Start () {
        CuadroGuardar.SetActive(false);
    }
    public void abrirCuadroGuardar() {
        CuadroGuardar.SetActive(true);
    }
	
	
}

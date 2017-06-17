using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class regresarModulo : MonoBehaviour {

    public void regresarModuloAnterior() {
        string modulo = ModulosController.modulo;
        SceneManager.LoadScene(modulo);
    }
}

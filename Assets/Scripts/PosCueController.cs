using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PosCueController : MonoBehaviour {

    public Image lockZen;
    public Image lockKok;


    void Start()
    {

        extraerLockMovimiento(3, lockZen);
        extraerLockMovimiento(4, lockKok);

    }
    public void extraerLockMovimiento(int movimiento, Image lockMov)
    {
        string url = "http://localhost/PAK_Modulos/PAK_getLockMov.php?" + "UsuSobNom=" + LoginController.usuario + "&NumMov=" + movimiento;
        StartCoroutine(LockMov(url, lockMov));
    }

    IEnumerator LockMov(string url, Image lockMov)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("null"))
        {
            Debug.Log("No se encuentran datos");
        }
        else
        {
            if (conecction.text.Contains("1"))
            {
                lockMov.enabled = false;

            }
        }
    }


    public void regresar()
    {
        SceneManager.LoadScene("ModulosEntrenamiento");
    }
    public void abrirKokutsu()
    {
        if (!lockKok.enabled)
        {
            SceneManager.LoadScene("KinectGesturesDemo");
        }
       
    }
    public void abrirZenkutsu()
    {
        if (!lockZen.enabled)
        {
            SceneManager.LoadScene("KinectGesturesDemo");
        }
        
    }
}

using UnityEngine;
using System.Collections;
using System;

public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    // GUI Text to display the gesture messages.
    public GUIText GestureInfo;
    public int contador1;
    public int contador2;
    public int contador3;
    public int contador4;
    public int contador5;
    public int contador6;
    public int contador7;
    public int contador8;

    private bool Musibidachi;
    private bool Joe;
    private bool Zenkutsu;
    private bool Kokutsu;
    private bool Tsuki;
    private bool OiZuki;
    private bool Uke;
    private bool AgeUke;

    public int MovActual = 0;
    private string usuarioTemp = LoginController.usuario;
    public static String modulo = "";

    void Awake()
    {
        extraerLockMovimiento(1);
        extraerLockMovimiento(2);
        extraerLockMovimiento(3);
        extraerLockMovimiento(4);
        extraerLockMovimiento(5);
        extraerLockMovimiento(6);
        extraerLockMovimiento(7);
        extraerLockMovimiento(8);
    }

    void Start()
    {
        SeleccionMovimineto();
    }

    public void SeleccionMovimineto()
    {
        if (MovActual == 1)
        {
            Musibidachi = true;
            contador1 = 0;
        }
        else if (MovActual == 2)
        {
            Joe = true;
            contador2 = 0;
        }
        else if (MovActual == 3)
        {
            Zenkutsu = true;
            contador3 = 0;
        }
        else if (MovActual == 4)
        {
            Kokutsu = true;
            contador4 = 0;
        }
        else if (MovActual == 5)
        {
            Tsuki = true;
            contador5 = 0;
        }
        else if (MovActual == 6)
        {
            OiZuki = true;
            contador6 = 0;
        }
        else if (MovActual == 7)
        {
            Uke = true;
            contador7 = 0;
        }
        else if (MovActual == 8)
        {
            AgeUke = true;
            contador8 = 0;
        }

    }

    public void extraerLockMovimiento(int movimiento)
    {
        string url = "http://localhost/PAK_Modulos/PAK_getLockMov.php?" + "UsuSobNom=" + LoginController.usuario + "&NumMov=" + movimiento;
        StartCoroutine(LockMov(url));
    }

    IEnumerator LockMov(string url)
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
                MovActual += 1;
            }
        }
    }




    public bool EsMusibidachi()
    {
        if (Musibidachi)
        {
            Musibidachi = false;
            return true;
        }

        return false;
    }
    public bool EsJoe()
    {
        if (Joe)
        {
            Joe = false;
            return true;
        }

        return false;
    }

    public bool EsZenkutsu()
    {
        if (Zenkutsu)
        {
            Zenkutsu = false;
            return true;
        }

        return false;
    }

    public bool EsKokutsu()
    {
        if (Kokutsu)
        {
            Kokutsu = false;
            return true;
        }

        return false;
    }

    public bool EsOiZuki()
    {
        if (OiZuki)
        {
            OiZuki = false;
            return true;
        }

        return false;
    }

    public bool EsTsuki()
    {
        if (Tsuki)
        {
            Tsuki = false;
            return true;
        }

        return false;
    }

    public bool EsUke()
    {
        if (Uke)
        {
            Uke = false;
            return true;
        }

        return false;
    }

    public bool EsAgeUke()
    {
        if (AgeUke)
        {
            AgeUke = false;
            return true;
        }

        return false;
    }

    public void UserDetected(uint userId, int userIndex)
    {
        // detect these user specific gestures
        KinectManager manager = KinectManager.Instance;

        manager.DetectGesture(userId, KinectGestures.Gestures.Musibidachi);
        manager.DetectGesture(userId, KinectGestures.Gestures.Joe);
        manager.DetectGesture(userId, KinectGestures.Gestures.Zenkutsu);
        manager.DetectGesture(userId, KinectGestures.Gestures.Kokutsu);
        manager.DetectGesture(userId, KinectGestures.Gestures.OiZuki);
        manager.DetectGesture(userId, KinectGestures.Gestures.Tsuki);
        manager.DetectGesture(userId, KinectGestures.Gestures.Uke);
        manager.DetectGesture(userId, KinectGestures.Gestures.AgeUke);

        if (GestureInfo != null)
        {
            GestureInfo.GetComponent<GUIText>().text = "Realice golpes hacia el frente";
        }
    }

    public void UserLost(uint userId, int userIndex)
    {
        if (GestureInfo != null)
        {
            GestureInfo.GetComponent<GUIText>().text = string.Empty;
        }
    }

    public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
        // don't do anything here
    }

    public bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
        string sGestureText = gesture + " Detectado";
        if (GestureInfo != null)
        {
            GestureInfo.GetComponent<GUIText>().text = sGestureText;
        }

        if (gesture == KinectGestures.Gestures.Musibidachi)
        {
            Musibidachi = true;
            contador1++;
        }
        else if (gesture == KinectGestures.Gestures.Joe)
        {
            Joe = true;
            contador2++;
        }
        else if (gesture == KinectGestures.Gestures.Zenkutsu)
        {
            Zenkutsu = true;
            contador3++;
        }
        else if (gesture == KinectGestures.Gestures.Kokutsu)
        {
            Kokutsu = true;
            contador4++;
        }
        else if (gesture == KinectGestures.Gestures.Tsuki)
        {
            Tsuki = true;
            contador5++;
        }
        else if (gesture == KinectGestures.Gestures.OiZuki)
        {
            OiZuki = true;
            contador6++;
        }
        else if (gesture == KinectGestures.Gestures.Uke)
        {
            Uke = true;
            contador7++;
        }
        else if (gesture == KinectGestures.Gestures.AgeUke)
        {
            AgeUke = true;
            contador8++;
        }

        if (MovActual == 1)
        {
            if (contador1 == 3)
                GestureInfo.GetComponent<GUIText>().text = "Siguiente Movimiento";
                ActualizarMovimiento(1);
        }
        else if (MovActual == 2)
        {
            if (contador2 == 3)
                GestureInfo.GetComponent<GUIText>().text = "Siguiente Movimiento";
                ActualizarMovimiento(2);
        }
        else if (MovActual == 3)
        {
            if (contador3 == 3)
                GestureInfo.GetComponent<GUIText>().text = "Siguiente Movimiento";
                ActualizarMovimiento(3);
        }
        else if (MovActual == 4)
        {
            if (contador4 == 3)
                GestureInfo.GetComponent<GUIText>().text = "Siguiente Movimiento";
                ActualizarMovimiento(4);
        }
        else if (MovActual == 5)
        {
            if (contador5 == 3)
                GestureInfo.GetComponent<GUIText>().text = "Siguiente Movimiento";
                ActualizarMovimiento(5);
        }
        else if (MovActual == 6)
        {
            if (contador6 == 3)
                GestureInfo.GetComponent<GUIText>().text = "Siguiente Movimiento";
                ActualizarMovimiento(6);
        }
        else if (MovActual == 7)
        {
            if (contador7 == 3)
                GestureInfo.GetComponent<GUIText>().text = "Siguiente Movimiento";
                ActualizarMovimiento(7);
        }
        else if (MovActual == 8)
        {
            if (contador8 == 3)
                GestureInfo.GetComponent<GUIText>().text = "Siguiente Movimiento";
                ActualizarMovimiento(8);
        }

        return true;

    }

    public void ActualizarMovimiento(int movimiento)
    {
        string url = "http://localhost/PAK_Modulos/PAK_setMov.php?" + "UsuSobNom=" + LoginController.usuario + "&NumMov=" + movimiento;
        StartCoroutine(Actualizar(url));
    }

    IEnumerator Actualizar(string url)
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
           
        }
    }



    public bool GestureCancelled(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint)
    {
        // don't do anything here, just reset the gesture state
        return true;
    }

}

using UnityEngine;
using System.Collections;
using System;

public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    // GUI Text to display the gesture messages.
    public GUIText GestureInfo;
    public int contador =0;
    private bool Musibidachi;
    private bool Joe;
    private bool Zenkutsu;
    private bool Kokutsu;
    private bool Tsuki;
    private bool OiZuki;
    private bool Uke;
    private bool AgeUke;

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
        manager.DetectGesture(userId, KinectGestures.Gestures.GolpeIzquierda);

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
            contador++;
        }
        else if (gesture == KinectGestures.Gestures.Joe)
        {
            Joe= true;
            contador++;
        }
        else if (gesture == KinectGestures.Gestures.Zenkutsu)
        {
            Zenkutsu = true;
            contador++;
        }
        else if (gesture == KinectGestures.Gestures.Kokutsu)
        {
            Kokutsu = true;
            contador++;
        }
        else if (gesture == KinectGestures.Gestures.Tsuki)
        {
            Tsuki = true;
            contador++;
        }
        else if (gesture == KinectGestures.Gestures.OiZuki)
        {
            OiZuki = true;
            contador++;
        }
        else if (gesture == KinectGestures.Gestures.Uke)
        {
            Uke = true;
            contador++;
        }
        else if (gesture == KinectGestures.Gestures.AgeUke)
        {
            AgeUke = true;
            contador++;
        }

        if (contador == 3)
        {
            GestureInfo.GetComponent<GUIText>().text = "Siguiente Movimiento";
        }




        Debug.Log(contador);
        return true;

    }

    public bool GestureCancelled(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint)
    {
        // don't do anything here, just reset the gesture state
        return true;
    }

}

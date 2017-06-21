using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class MusibidachiListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;

    private bool Musibidachi;
    private int contador=0;
    


    public bool EsMusibidachi()
    {
        if (Musibidachi)
        {
            Musibidachi = false;
            return true;
        }

        return false;
    }




    public void UserDetected(uint userId, int userIndex)
	{
		// detect these user specific gestures
		KinectManager manager = KinectManager.Instance;

        manager.DetectGesture(userId, KinectGestures.Gestures.Musibidachi);

        if (GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = "Realice movimientos";
		}
	}

	public void UserLost(uint userId, int userIndex)
	{
		if(GestureInfo != null)
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

                if (gesture == KinectGestures.Gestures.Musibidachi) {
                    Musibidachi = true;
                    contador++;
                    Debug.Log(contador);
        }
                            
        if (contador == 3)
        {
            SceneManager.LoadScene("Tsuki");
        }

        return true;
	}

	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture,
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
		// don't do anything here, just reset the gesture state
		return true;
	}

}

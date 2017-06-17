using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class ConfigurationController : MonoBehaviour {
    public Toggle vozEntrenamientoCheck;
    public Toggle mostrarSubsCheck;
    public Toggle mostrarPantaCheck;
    public Toggle mostrarTipsCheck;
    public Dropdown ambiente;
    public Dropdown musica;
    public Image subWelcome;
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    public AudioSource audio4;
    public AudioSource audio5;
    public AudioSource audioBienvenida;
    public Slider volume;    
    public float timePlay = 10.0F;
    
   
    //public string usuarioTemp = "123475";
    private string usuarioTemp = LoginController.usuario;

    // Use this for initialization
    void Start () {
        extraerVozEntrenador();
        extraerMostrarSubs();
        extraerMostrarPanta();
        extraerMostrarTips();
        extraerAmbiente();
        extraerMusica();

    }

    public void setVolume() {
        LoginController.volumenSelected = volume.value;
        Debug.Log("volume=" + LoginController.volumenSelected);
        AudioListener.volume = volume.value;
    }

    public void reproducirMusica() {
        LoginController.musicSelected = musica.value + 1;
            
        switch (LoginController.musicSelected) {
            case 1:
                stopMusic();
                audio1.Play();
                audio1.SetScheduledEndTime(AudioSettings.dspTime + timePlay);
                break;
            case 2:
                stopMusic();
                audio2.Play();
                audio2.SetScheduledEndTime(AudioSettings.dspTime + timePlay);
                break;
            case 3:
                stopMusic();
                audio3.Play();
                audio3.SetScheduledEndTime(AudioSettings.dspTime + timePlay);
                break;
            case 4:
                stopMusic();
                audio4.Play();
                audio4.SetScheduledEndTime(AudioSettings.dspTime + timePlay);
                break;
            case 5:
                stopMusic();
                audio5.Play();
                audio5.SetScheduledEndTime(AudioSettings.dspTime + timePlay);
                break;
            default:
                break;
        }
    }
    public void stopMusic() {
        audio1.Stop();
        audio2.Stop();
        audio3.Stop();
        audio4.Stop();
        audio5.Stop();
    }



    public void extraerMusica()
    {
        string url = "http://localhost/PAK_Configuraciones/PAK_getMusic.php?" + "UsuSobNom=" + usuarioTemp;
        StartCoroutine(Music(url));
    }

    IEnumerator Music(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("Query"))
        {
            Debug.Log("No se encuentran datos");

        }
        else
        {
            musica.value = Int32.Parse(conecction.text) - 1;
            LoginController.musicSelected = Int32.Parse(conecction.text) - 1;
        }
    }

    public void setMusica()
    {
        string url = "http://localhost/PAK_Configuraciones/PAK_setMusic.php?" + "UsuSobNom=" + usuarioTemp;

        int index = musica.value + 1;
        LoginController.musicSelected = index;
        url = url + "&MusCod=" + index;
        StartCoroutine(setMusic(url));
    }

    IEnumerator setMusic(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("Query"))
        {
            Debug.Log("Error al modificar");
        }


    }


        public void extraerAmbiente()
    {
        string url = "http://localhost/PAK_Configuraciones/PAK_getTrainingPlace.php?" + "UsuSobNom=" + usuarioTemp;
        StartCoroutine(TrainingPlace(url));
    }

    IEnumerator TrainingPlace(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("Query"))
        {
            Debug.Log("No se encuentran datos");
            
        }
        else
        {
            ambiente.value = Int32.Parse(conecction.text)-1;
            LoginController.dojoSelected = Int32.Parse(conecction.text) - 1;

        }
    }

    public void setAmbiente()
    {
        string url = "http://localhost/PAK_Configuraciones/PAK_setTrainingPlace.php?" + "UsuSobNom=" + usuarioTemp;
        
        int index = ambiente.value+1;
        LoginController.dojoSelected = index;
        url = url + "&AmbCod=" + index;        
        StartCoroutine(setTrainingPlace(url));

    }

    IEnumerator setTrainingPlace(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("Query"))
        {
            Debug.Log("Error al modificar");
        }

    }


    public void extraerMostrarTips()
    {
        string url = "http://localhost/PAK_Configuraciones/PAK_getShowTips.php?" + "UsuSobNom=" + usuarioTemp;
        StartCoroutine(ShowTips(url));
    }

    IEnumerator ShowTips(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("0"))
        {
            mostrarTipsCheck.isOn = false;
        }
        else
        {
            if (conecction.text.Contains("1"))
            {
                mostrarTipsCheck.isOn = true;
            }
            else
            {
                Debug.Log("No se encuentran datos");
            }
        }
    }
    public void setMostrarTips()
    {
        string url = "http://localhost/PAK_Configuraciones/PAK_setShowTips.php?" + "UsuSobNom=" + usuarioTemp;
        if (mostrarTipsCheck.isOn)
        {
            url = url + "&PerMosTip=" + "1";
            LoginController.mostrarTips = 1;
        }
        else
        {
            url = url + "&PerMosTip=" + "0";
            LoginController.mostrarTips = 0;
        }

        StartCoroutine(setShowTips(url));
    }

    IEnumerator setShowTips(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("Query"))
        {
            Debug.Log("Error al modificar");
        }


    }



    public void extraerMostrarPanta()
    {
        
        string url = "http://localhost/PAK_Configuraciones/PAK_getShowMe.php?" + "UsuSobNom=" + usuarioTemp;
        StartCoroutine(ShowMe(url));
    }

    IEnumerator ShowMe(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("0"))
        {
            mostrarPantaCheck.isOn = false;
        }
        else
        {
            if (conecction.text.Contains("1"))
            {
                mostrarPantaCheck.isOn = true;
            }
            else
            {
                Debug.Log("No se encuentran datos");
            }
        }
    }


    public void setMostrarPanta()
    {
        string url = "http://localhost/PAK_Configuraciones/PAK_setShowMe.php?" + "UsuSobNom=" + usuarioTemp;
        if (mostrarPantaCheck.isOn)
        {
            url = url + "&PerMosPan=" + "1";
            LoginController.mostrarmePantalla = 1;
        }
        else
        {
            url = url + "&PerMosPan=" + "0";
            LoginController.mostrarmePantalla = 0;
        }

        StartCoroutine(setShowMe(url));
    }

    IEnumerator setShowMe(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("Query"))
        {
            Debug.Log("Error al modificar");
        }

    }
    

    public void extraerMostrarSubs()
    {

        //    string url = "http://localhost/PAK_Configuraciones/PAK_getShowSubs.php?" + "UsuSobNom=" + LoginController.usuario;
        string url = "http://localhost/PAK_Configuraciones/PAK_getShowSubs.php?" + "UsuSobNom=" + usuarioTemp;
        StartCoroutine(ShowSubs(url));
    }

    IEnumerator ShowSubs(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("0"))
        {
            mostrarSubsCheck.isOn = false;
        }
        else
        {
            if (conecction.text.Contains("1"))
            {
                mostrarSubsCheck.isOn = true;
                subWelcome.enabled = true;
            }
            else
            {
                subWelcome.enabled = false;
                Debug.Log("No se encuentran datos");
            }
        }
    }

    public void setMostrarSubs()
    {
        string url = "http://localhost/PAK_Configuraciones/PAK_setShowSubs.php?" + "UsuSobNom=" + usuarioTemp;
        if (mostrarSubsCheck.isOn)
        {
            subWelcome.enabled = true;
            url = url + "&PerMosSub=" + "1";
            LoginController.mostrarSubs = 1;
        }
        else
        {
            subWelcome.enabled = false;
            url = url + "&PerMosSub=" + "0";
            LoginController.mostrarSubs = 0;
        }

        StartCoroutine(setShowSubs(url));
    }

    IEnumerator setShowSubs(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("Query"))
        {
            Debug.Log("Error al modificar");
        }

    }

    

    public void setVozEntrenador()
    {
        string url = "http://localhost/PAK_Configuraciones/PAK_SetTrainingVoice.php?" + "UsuSobNom=" + usuarioTemp;
        if (vozEntrenamientoCheck.isOn)
        {
            audioBienvenida.Play();
            url = url + "&PerRepVoz=" + "1";
            LoginController.reproducirVoz = 1;
        }
        else {
            url = url + "&PerRepVoz=" + "0";
            LoginController.reproducirVoz = 0;
        }
           


        StartCoroutine(SetTrainingVoice(url));
    }

    IEnumerator SetTrainingVoice(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("Query"))
        {
            Debug.Log("Error al modificar");
        }

    }

    public void extraerVozEntrenador() {

        
        string url = "http://localhost/PAK_Configuraciones/PAK_getTrainingVoice.php?" + "UsuSobNom=" + usuarioTemp;
        StartCoroutine(TrainingVoice(url));
    }

    IEnumerator TrainingVoice(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("0"))
        {
            vozEntrenamientoCheck.isOn = false;
        }
        else
        {
            if (conecction.text.Contains("1"))
            {
                vozEntrenamientoCheck.isOn = true;
            }
            else {
                Debug.Log("No se encuentran datos");
            }
        }
    }

}

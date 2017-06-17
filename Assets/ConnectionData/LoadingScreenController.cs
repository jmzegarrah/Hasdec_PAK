using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingScreenController : MonoBehaviour {
    public Sprite img1;
    public Sprite img2;
    public Sprite img3;
    public Sprite img4;
    public Sprite img5;
    public Image imgTop;
    public Image imgBot;
    public Text historia;
    public Text recomendacion;
    public float DelayTime = 5.0f;

    // Use this for initialization
    void Start () {
        if (LoginController.mostrarTips == 1)
        {
            setHistoria();
            setRecomendacion();
            StartCoroutine("Wait");
        }
        else {
            imgTop.enabled = false;
            imgBot.enabled = false;
            StartCoroutine("Wait");
        }
       
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(DelayTime);

        string modulo = ModulosController.modulo;
        switch (modulo) {
            case "PosNaturales":
                SceneManager.LoadScene("PosNaturales", LoadSceneMode.Single);
                break;
            case "Cuerpo":
                SceneManager.LoadScene("Cuerpo", LoadSceneMode.Single);
                break;
            case "Ataque":
                SceneManager.LoadScene("Ataque", LoadSceneMode.Single);
                break;
            case "Defensa":
                SceneManager.LoadScene("Defensa", LoadSceneMode.Single);
                break;
            default:
                break;
         }
       
    }
    public void setHistoria() {
        string url = "http://localhost/PAK_Loading/PAK_getHistory.php?";
        StartCoroutine(History(url));

    }

    public void setRecomendacion()
    {
        string url = "http://localhost/PAK_Loading/PAK_getTip.php?";
        StartCoroutine(Tip(url));

    }

    IEnumerator History(string url)
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
            string index = conecction.text.Substring(0,1);
            historia.text = conecction.text.Substring(2);
            
            switch (index) {
                case "1":
                    GameObject.Find("Panel").GetComponent<Image>().sprite = img1;
                    break;
                case "2":
                    GameObject.Find("Panel").GetComponent<Image>().sprite = img2;
                    break;
                case "3":
                    GameObject.Find("Panel").GetComponent<Image>().sprite = img3;
                    break;
                case "4":
                    GameObject.Find("Panel").GetComponent<Image>().sprite = img4;
                    break;
                case "5":
                    GameObject.Find("Panel").GetComponent<Image>().sprite = img5;
                    break;
                default:
                    break;
            }
            
        }
    }
    IEnumerator Tip(string url)
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
            Debug.Log("TIP");
            Debug.Log(recomendacion.text.Length);
              recomendacion.text = conecction.text;
        }
    }

}

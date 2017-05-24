using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {
    public GameObject tutorial;
    

    // Use this for initialization
    void Start () {
        tutorial.SetActive(false);
        string url = "http://localhost/PAK_Tutorial/PAK_mostrarTutorial.php?"
                         + "UsuSobNom=" + LoginController.usuario;
        StartCoroutine(showTutorial(url));
    }
    public void showTutorial() {
        StartCoroutine(showTutorialClick());

    }
    IEnumerator showTutorialClick()
    {
            tutorial.SetActive(true);
            yield return new WaitForSeconds(10);
            tutorial.SetActive(false);
    }
    IEnumerator showTutorial(string url)
    {
        Debug.Log(url);
        WWW conecction = new WWW(url);
        yield return (conecction);
        if (conecction.text.Contains("0"))
        {
            
        }
        else
        {
            tutorial.SetActive(true);           
            yield return new WaitForSeconds(10);
            tutorial.SetActive(false);
           

        }
    }
}
